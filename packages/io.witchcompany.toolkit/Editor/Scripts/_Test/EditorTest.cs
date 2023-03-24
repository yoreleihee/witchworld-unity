using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.Tool;
using WitchCompany.Toolkit.Editor.Validation;

namespace WitchCompany.Toolkit.Editor
{
    public static class EditorTest
    {
        [MenuItem("WitchToolkit/Test")]
        private static void Test()
        {
            // var option = AssetTool.GetSelectedAsset() as BlockOption;

            // Debug.Log(UploadRuleValidator.ValidateBundleSize(option));
            
            Debug.Log(OptimizationValidator.ValidationCheck().result);
            
            foreach (var msg in OptimizationValidator.ValidationCheck().errMessages)
            {
                Debug.Log(msg);
            }
            
        }
    }
}