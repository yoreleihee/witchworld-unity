using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.Tool;
using Object = System.Object;

namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class OptimizationValidator
    {
        private static List<Tuple<string, int>> failedObjects = new();
        /// <summary>
        /// 최적화 관련 유효성 검사 -> 개별 함수 작성 필요
        /// - 씬에 배치된 오브젝트 전체 가져오기 v
        /// - 씬에 배치된 개별 매쉬 버텍스 개수 검사 v
        /// - 씬에 배치된 전체 메쉬 버텍스 개수 검사 v
        /// - 씬에서 사용된 라이트맵 용량 검사 v
        /// - 씬에서 사용된 텍스쳐 용량 검사 v
        /// - 씬에서 유니크 머테리얼 개수 검사 v 
        /// </summary>
        public static ValidationReport ValidationCheck()
        {
            failedObjects.Clear();
            
            // 유효성 검사 객체 생성
            var validationReport = new ValidationReport();

            // 전체 버텍스 검사
            if (GetMeshVertex() > OptimizationConfig.MAX_VERTS)
            {
                validationReport.Append("모든 Mesh의 Vertex 개수가 최대값 이상입니다.");
            }
            
            // 개별 버텍스 검사
            if (failedObjects.Count > 0)
            {
                foreach (var obj in failedObjects)
                {
                    validationReport.Append($"해당 Mesh의 Vertex 개수가 최대값 이상입니다.\n- Path : {obj.Item1} \n- Vertex: {obj.Item2}");
                }
            }

            //  라이트맵 검사
            if (GetLightMapMB() > OptimizationConfig.MAX_LIGHTMAP_MB)
            {
                validationReport.Append("LightMap 최대 용량 이상입니다.");
            }
            
            //  텍스쳐 검사
            if (GetTextureMB() > OptimizationConfig.MAX_SHARED_TEXTURE_MB)
            {
                validationReport.Append("Texture 최대 용량 이상입니다.");
            }
            
            //  유니크 머티리얼 검사
            if (GetUniqueMaterialCount() > OptimizationConfig.MAX_UNIQUE_MATERIALS)
            {
                validationReport.Append("Material 최대 개수 이상입니다.");
            }
            
            // 리포트 오류 메시지가 있으면 Failed 
            if(validationReport.errMessages.Count > 0)
                validationReport.result = ValidationReport.Result.Failed;

            return validationReport;
        }

        /// <summary> 씬에 배치된 Renderer 오브젝트 전체 가져오기 </summary>
        public static Renderer[] GetAllRenderer()
        {
            return GameObject.FindObjectsOfType<Renderer>(true);
        }
        
        // public static T[] GetAllGameObjectT<T>()
        // {
        //     return GameObject.FindObjectsOfType<T>(true);
        // }

        /// <summary> 개별 매쉬 버텍스 개수 </summary>
        public static int GetIndividualMeshVertex(Renderer renderer, List<Mesh> meshes)
        {
            int vertexCount = 0;
            
            // renderer에서 mesh 탐색 -> 찾은 mesh 리스트에 추가
            // 모든 Vertex 개수 구함
            if (renderer is MeshRenderer)
            {
                var filter = renderer.GetComponent<MeshFilter>();
                if (filter != null && filter.sharedMesh != null)
                {
                    vertexCount += filter.sharedMesh.vertexCount;
                    meshes.Add(filter.sharedMesh);
                }
            }
            else if (renderer is SkinnedMeshRenderer)
            {
                var skinned = renderer as SkinnedMeshRenderer;
                if (skinned.sharedMesh != null)
                {
                    vertexCount += skinned.sharedMesh.vertexCount;
                    meshes.Add(skinned.sharedMesh);
                }
            }
            else if (renderer is BillboardRenderer)
            {
                vertexCount += 4;
            }
            
            return vertexCount;
        }

        /// <summary> 전체 매쉬 버텍스 개수 (중복 제거 X)</summary>
        public static int GetMeshVertex()
        {
            var foundMeshes = new List<Mesh>();
            
            int totalVertexCount = 0;

            // 모든 게임 오브젝트 중 모든 랜더러를 배열에 저장
            var renderers = GetAllRenderer();
            
            // 랜더러 배열 탐색
            foreach (var renderer in renderers)
            {
                int vertex = 0;
                if (renderer is MeshRenderer)
                {
                    MeshFilter filter = renderer.GetComponent<MeshFilter>();
                    if (filter != null && filter.sharedMesh != null)
                    {
                        foundMeshes.Add(filter.sharedMesh);
                        vertex = filter.sharedMesh.vertexCount;
                        totalVertexCount += vertex;
                    }
                }
                else if (renderer is SkinnedMeshRenderer)
                {
                    SkinnedMeshRenderer skinned = renderer as SkinnedMeshRenderer;
                    if (skinned.sharedMesh != null)
                    {
                        foundMeshes.Add(skinned.sharedMesh);
                        totalVertexCount += skinned.sharedMesh.vertexCount;
                        totalVertexCount += vertex;
                    }
                }
                else if (renderer is BillboardRenderer)
                {
                    totalVertexCount += 4;
                }
            }
            
            // 찾은 mesh 중 중복 제거
            IEnumerable<Mesh> uniqueMeshes = foundMeshes.Distinct();

            failedObjects.AddRange(uniqueMeshes.Select(m => new Tuple<string, int>(AssetDatabase.GetAssetPath(m)+$"/{m.name}", m.vertexCount)).Where(m => m.Item2 > OptimizationConfig.MAX_INDIVIDUAL_VERTS));

            return totalVertexCount;
        }

        /// <summary> 라이트맵 용량 </summary>
        public static int GetLightMapMB()
        {
            long bytes = 0;
            var lightmaps = LightmapSettings.lightmaps;
            foreach (var lightmap in lightmaps)
            {
                if (lightmap.lightmapColor != null)
                {
                    long sizeInBytes = Profiler.GetRuntimeMemorySizeLong(lightmap.lightmapColor);
                    bytes += sizeInBytes;
                    
                }
                if (lightmap.lightmapDir != null)
                {
                    long sizeInBytes = Profiler.GetRuntimeMemorySizeLong(lightmap.lightmapDir);
                    bytes += sizeInBytes;
                    
                }
                if (lightmap.shadowMask != null)
                {
                    long sizeInBytes = Profiler.GetRuntimeMemorySizeLong(lightmap.shadowMask);
                    bytes += sizeInBytes;
                }
            }
            return (int)bytes / 1024 / 1024;
        }
        
        
        /// <summary> 텍스쳐 용량 </summary>
        public static int GetTextureMB()
        {
            var foundTextures = new List<Texture>();
            var renderers  = GetAllRenderer();

            foreach (Renderer renderer in renderers)
            {
                // 랜더러의 shardMaterial 배열 탐색
                foreach (var material in renderer.sharedMaterials)
                {
                    if (material == null)
                        continue;

                    // 머티리얼의 texture 배열 탐색 -> texture 찾아서 리스트에 추가 (foundTextures)
                    foreach (var texName in material.GetTexturePropertyNames())
                    {
                        var tex = material.GetTexture(texName);
                        if (tex != null)
                        {
                            foundTextures.Add(tex);
                        }
                    }
                }
            }
            
            // 텍스트 사이즈 계산 - 중복 제거
            long bytes = 0;
            foreach (Texture texture in foundTextures.Distinct())
            {
                long sizeInBytes = Profiler.GetRuntimeMemorySizeLong(texture);
                bytes += sizeInBytes;
            }
            
            return (int)(bytes / 1024 / 1024);;
        }

        /// <summary> 유니크 머테리얼 개수 </summary>
        public static int GetUniqueMaterialCount()
        {
            List<Material> materials = new List<Material>();
            
            Renderer[] renderers = GetAllRenderer();
            
            foreach (Renderer renderer in renderers)
            {
                // 머티리얼 리스트에 랜더러의 shardMaterial 추가
                materials.AddRange(renderer.sharedMaterials);
            }
            
            return materials.FindAll(m => m != null).Select(m => m.name).Distinct().Count();;
        }

    }
}