using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
using Object = System.Object;

namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class OptimizationValidator
    {
        // TODO : 최대 개수 지정 필요 (현재는 임시로 지정함)
        private const int K = 1000;
        
        public static int MAX_VERTS = 1 * K;
        public static int MAX_TOTAL_VERTS = 500 * K;
        public static int MAX_UNIQUE_MATERIALS = 75;
        public static int MAX_SHARED_TEXTURE_MB = 200;
        
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
            // TODO: 여기를 수정하세요

            var validationReport = new ValidationReport();
            
            // 버텍스 검사
            if (GetAllMeshVertex() > MAX_TOTAL_VERTS)
            {
                // validationReport.errorMsg
            }
            
            
            
            return new ValidationReport()
            {
                result = ValidationReport.Result.Success,
                errMessages = null
            };
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

        /// <summary> 개별 매쉬 버텍스 개수 검사 </summary>
        public static int GetMeshVertex(Renderer renderer)
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
                }
            }
            else if (renderer is SkinnedMeshRenderer)
            {
                var skinned = renderer as SkinnedMeshRenderer;
                if (skinned.sharedMesh != null)
                {
                    vertexCount += skinned.sharedMesh.vertexCount;
                }
            }
            else if (renderer is BillboardRenderer)
            {
                vertexCount += 4;
            }
            
            return vertexCount;
        }
        
        /// <summary> 전체 매쉬 버텍스 개수 검사 (중복 제거 X)</summary>
        public static int GetAllMeshVertex()
        {
            int totalVertexCount = 0;

            // 모든 게임 오브젝트 중 모든 랜더러를 배열에 저장
            var renderers = GetAllRenderer();
            
            // 랜더러 배열 탐색
            foreach (var renderer in renderers)
            {
                totalVertexCount += GetMeshVertex(renderer);
            }
            return totalVertexCount;
        }

        /// <summary> 라이트맵 용량 검사 </summary>
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
        
        
        /// <summary> 텍스쳐 용량 검사 </summary>
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

        /// <summary> 유니크 머테리얼 개수 검사 </summary>
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