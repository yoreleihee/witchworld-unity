using UnityEditor;
using UnityEngine;

namespace WitchCompany.Toolkit.Editor.EditorTool
{
    public static class AssetTool
    {
        public static Object GetSelectedAsset()
        {
            return Selection.GetFiltered(typeof(Object), SelectionMode.Assets)?[0];
        }
        
        public static Object[] GetSelectedAssets()
        {
            return Selection.GetFiltered(typeof(Object), SelectionMode.Deep);
        }

        /// <summary>
        /// <paramref name="assetPath"/>에 있는 에셋을 AssetImporter 형식으로 가져온다.
        /// </summary>
        /// <param name="assetPath">"Assets/..." 에 해당하는 경로</param>
        public static AssetImporter GetAssetImporterAtPath(string assetPath)
        {
            // 예외처리
            if (string.IsNullOrEmpty(assetPath) || !assetPath.StartsWith("Assets/"))
            {
                Debug.LogError("잘못된 assetPath입니다: " + assetPath);
                return null;
            }
            
            // 에셋 찾기
            var assetImporter = AssetImporter.GetAtPath(assetPath);

            // 예외처리
            if (assetImporter == null) 
                Debug.LogError("없는 에셋입니다: " + assetPath);

            return assetImporter;
        }
    }
}