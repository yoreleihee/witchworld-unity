using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.BundleTool;
using WitchCompany.Toolkit.Editor.EditorTool;

namespace WitchCompany.Toolkit.Editor
{
    public static class EditorTest
    {
        [MenuItem("WitchToolkit/Test")]
        private static void Test()
        {
            var asset = AssetTool.GetSelectedAsset();
            var path = AssetDatabase.GetAssetPath(asset);
            Debug.Log(path);
            AssetBundleTool.AssignAssetBundle(path, "test_bundle");
        }
    }
}