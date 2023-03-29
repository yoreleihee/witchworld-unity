using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;

namespace WitchCompany.Toolkit.Editor.Tool
{
    /// <summary>
    /// 에셋번들 관련 툴 모음
    /// </summary>
    public static class AssetBundleTool
    {
        /// <summary>assetPath에 있는 에셋을 bundleName을 가진 에셋번들로 할당한다.</summary>
        /// <param name="assetPath">"Assets/..." 에 해당하는 경로</param>
        /// <param name="bundleName">번들 이름</param>
        public static void AssignAssetBundle(string assetPath, string bundleName)
        {
            if (string.IsNullOrEmpty(assetPath))
                throw new Exception("assetPath is null or empty");
            if (string.IsNullOrEmpty(bundleName))
                throw new Exception("bundleName is null or empty");
            
            // 에셋 가져오기
            var assetImporter = AssetTool.GetAssetImporterAtPath(assetPath);
            if (assetImporter == null)
                throw new Exception($"{assetPath}에 에셋이 없습니다.");

            // 에셋 번들 중복 확인
            if (!string.IsNullOrEmpty(assetImporter.assetBundleName)) 
                Debug.LogWarning($"에셋 번들을 덮어 씁니다. {assetImporter.assetBundleName} -> {bundleName}");
            // 에셋 번들 쓰기
            assetImporter.assetBundleName = bundleName;
        }

        /// <summary>프로젝트의 모든 에셋 번들을 지운다.</summary>
        public static void ClearAllBundles()
        {
            // 모든 에셋 번들 이름 가져오기
            var allAssetBundleNames = AssetDatabase.GetAllAssetBundleNames();

            if (allAssetBundleNames.Length <= 0)
                return;

            // 각 에셋 번들 이름에 대해
            foreach (var assetBundleName in allAssetBundleNames)
            {
                // 에셋 번들 이름으로 지정된 모든 에셋 경로 가져오기
                var assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleName);

                // 각 에셋 경로에 대해
                foreach (var assetPath in assetPaths)
                {
                    // 에셋의 AssetImporter 가져오기
                    var assetImporter = AssetImporter.GetAtPath(assetPath);

                    // 에셋의 에셋 번들 이름 지우기
                    assetImporter.assetBundleName = null;
                    assetImporter.SaveAndReimport();
                }
            }

            // 모든 사용되지 않는 에셋 번들 이름 지우기
            AssetDatabase.RemoveUnusedAssetBundleNames();
        }

        /// <summary>번들을 타겟 플렛폼으로 빌드한다.</summary>
        public static AssetBundleManifest BuildAssetBundle(BuildTarget target)
        {
            // 디렉토리
            var directory = AssetBundleConfig.BuildExportPath;

            // 디렉토리가 없으면 새로 만든다.
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            
            // 빌드 시작
            return BuildPipeline.BuildAssetBundles(directory, 
                BuildAssetBundleOptions.ForceRebuildAssetBundle| BuildAssetBundleOptions.ChunkBasedCompression, 
                target);
        }
    }
}