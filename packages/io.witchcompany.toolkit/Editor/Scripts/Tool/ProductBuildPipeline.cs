using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;

namespace WitchCompany.Toolkit.Editor.Tool
{
    public static class ProductBuildPipeline
    {
        public static JBuildReport PublishWithBuildReport()
        {
            // 리포트 생성
            var buildReport = new JBuildReport
            {
                result = JBuildReport.Result.Failed,
                BuildStatedAt = DateTime.Now
            };

            try
            {
                Debug.Log("상품 번들 추출 시작");
                
                //// 에셋번들 빌드
                // 번들 전부 지우기
                AssetBundleTool.ClearAllBundles();
                
                // 번들 할당
                var bundleName = $"{ProductConfig.Prefab.name}.bundle";
                AssetBundleTool.AssignAssetBundle(ProductConfig.PrefabPath, bundleName);
                
                // 번들 빌드
                var bundles = BuildAssetBundle();
                Debug.Log("상품 번들 빌드 성공");

                
                // 빌드 리포트 작성
                buildReport.result = JBuildReport.Result.Success;
                buildReport.buildGroups = new List<JBuildGroup>();
                foreach (var (target, bundle) in bundles)
                {
                    var group = new JBuildGroup
                    {
                        target = target,
                        exportPath = Path.Combine(ProductConfig.BundleExportPath, ProductConfig.Prefab.name)
                    };
                    group.totalSizeByte = AssetTool.GetFileSizeByte(ProductConfig.PrefabPath);
                }
                
                buildReport.BuildEndedAt = DateTime.Now;
            
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            
            return buildReport;
        }


        /// <summary>번들을 타겟 플렛폼으로 빌드한다.</summary>
        private static Dictionary<string, string[]> BuildAssetBundle()
        {
            try
            {
                var result = new Dictionary<string, string[]>();
                
                // WebGL
                EditorUserBuildSettings.webGLBuildSubtarget = WebGLTextureSubtarget.DXT;
                result.Add(AssetBundleConfig.Webgl, BuildBundle(AssetBundleConfig.Webgl,BuildTarget.WebGL));
    
                // // WebGL Mobile
                EditorUserBuildSettings.webGLBuildSubtarget = WebGLTextureSubtarget.ASTC;
                result.Add(AssetBundleConfig.WebglMobile, BuildBundle(AssetBundleConfig.WebglMobile,BuildTarget.WebGL));

                return result;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }
        
        private static string[] BuildBundle(string targetPlatform, BuildTarget target)
        {
            try
            {
                const BuildAssetBundleOptions option = BuildAssetBundleOptions.ForceRebuildAssetBundle |
                                                       BuildAssetBundleOptions.ChunkBasedCompression;
                // 폴더 경로
                var path = Path.Combine(ProductConfig.BundleExportPath, ProductConfig.Prefab.name, targetPlatform);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                var result = BuildPipeline.BuildAssetBundles(path, option, target);

                // 
                var deleteFile = Path.Combine(path, targetPlatform);
                File.Delete(deleteFile);
                File.Delete(deleteFile + ".manifest");    

                return result.GetAllAssetBundles();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }
    }
}