using System.IO;
using UnityEditor;
using UnityEngine;

namespace WitchCompany.Toolkit.Editor.Tool
{
    public static class AssetTool
    {
        /// <summary>현재 선택된 에셋 하나의 데이터를 가져온다.</summary>
        public static Object GetSelectedAsset() => 
            Selection.GetFiltered(typeof(Object), SelectionMode.Assets)?[0];

        /// <summary>현재 선택된 에셋 전체를 가져온다.</summary>
        public static Object[] GetSelectedAssets() => 
            Selection.GetFiltered(typeof(Object), SelectionMode.Deep);

        /// <summary>assetPath에 있는 에셋을 AssetImporter 형식으로 가져온다.</summary>
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

        /// <summary>해당 경로 파일의 사이즈를 구한다.</summary>
        public static long GetFileSizeByte(string filePath)
        {
            if (File.Exists(filePath))
            {
                var fileInfo = new FileInfo(filePath);
                return fileInfo.Length;
            }

            Debug.LogError("파일을 찾을 수 없습니다: " + filePath);
            return -1;
        }
    }
}