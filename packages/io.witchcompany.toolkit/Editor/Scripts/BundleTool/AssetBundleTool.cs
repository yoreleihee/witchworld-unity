using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.EditorTool;

namespace WitchCompany.Toolkit.Editor.BundleTool
{
    public static class AssetBundleTool
    {
        /// <summary>
        /// <paramref name="assetPath"/> 있는 에셋을 <paramref name="bundleName"/>을 가진 에셋번들로 설정한다.
        /// </summary>
        /// <param name="assetPath">"Assets/..." 에 해당하는 경로</param>
        /// <param name="bundleName">번들 이름</param>
        public static void AssignAssetBundle(string assetPath, string bundleName)
        {
            // 에셋 가져오기
            var assetImporter = AssetTool.GetAssetImporterAtPath(assetPath);

            // 에셋 번들 중복 확인
            if (!string.IsNullOrEmpty(assetImporter.assetBundleName)) 
                Debug.LogWarning($"에셋 번들을 덮어 씁니다. {assetImporter.assetBundleName} -> {bundleName}");
            
            // 에셋 번들 쓰기
            assetImporter.assetBundleName = bundleName;
        }

        public static void ClearAllBundles()
        {
            
        }
    }
}