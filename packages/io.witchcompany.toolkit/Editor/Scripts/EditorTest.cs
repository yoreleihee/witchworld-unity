using UnityEditor;
using WitchCompany.Toolkit.Editor.Tool;

namespace WitchCompany.Toolkit.Editor
{
    public static class EditorTest
    {
        [MenuItem("WitchToolkit/Test")]
        private static void Test()
        {
            AssetBundleTool.ClearAllBundles();
        }
    }
}