using System.IO;
using UnityEditor;

namespace WitchCompany.Toolkit.Editor.BundlePacker
{
    public static class AssetBundlePacker
    {
        [MenuItem("WitchToolkit/Build")]
        private static void BuildAssetBundle()
        {
            var directory = "./MyBundle";

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            BuildPipeline.BuildAssetBundles(directory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

            EditorUtility.DisplayDialog("에셋 번들 빌드", "빌드 성공", "닫기");
        }
    }
}