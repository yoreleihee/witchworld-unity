using System.IO;
using UnityEditor;
using UnityEngine;

namespace WitchCompany.Toolkit.Editor.BundleTool
{
    public static class AssetBundleBuilder
    {
        /// <summary>
        /// "WitchToolkit/Build" 메뉴 항목을 클릭하면 실행되는 함수로, 에셋 번들을 빌드하고 "./MyBundle" 디렉토리에 저장합니다.
        /// 에셋 번들 빌드가 완료되면, 사용자에게 "빌드 성공" 메시지가 표시됩니다.
        /// </summary>
        [MenuItem("WitchToolkit/Build")]
        private static void BuildAssetBundle()
        {
            
            const string directory = AssetBundleConfig.BuildExportPath;

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            
            var manifest = BuildPipeline.BuildAssetBundles(directory, 
                BuildAssetBundleOptions.ForceRebuildAssetBundle| BuildAssetBundleOptions.ChunkBasedCompression,
                BuildTarget.StandaloneWindows64);
            
            // foreach (var allDependency in manifest.GetAllDependencies("ryu"))
            // {
            //     Debug.Log(allDependency);
            // }
            
            EditorUtility.DisplayDialog("에셋 번들 빌드", "빌드 성공", "닫기");
        }
    }
}