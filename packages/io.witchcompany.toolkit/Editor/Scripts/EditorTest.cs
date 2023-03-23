using UnityEditor;
using UnityEditor.PackageManager;
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
            // 에셋 가져오기
            var test = AssetImporter.GetAtPath(
                "C:\\Workspace\\github.com\\witchcompany\\witchworld-unity\\packages\\io.witchcompany.resources\\Runtime\\MatPack");
            
        }
    }
}