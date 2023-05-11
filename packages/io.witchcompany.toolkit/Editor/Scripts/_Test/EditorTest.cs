using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.API;
using WitchCompany.Toolkit.Editor.Configs;

namespace WitchCompany.Toolkit.Editor
{
    public static class EditorTest
    {
        [MenuItem("WitchToolkit/Get BlockId")]
        public static async void GetBlockId()
        {
            var result = await WitchAPI.GetBlock(AdminConfig.PathName);

            var mode = ToolkitConfig.DeveloperMode ? "Dev" : "Prod";
            Debug.Log($"{mode}의 blockId : {result.blockId}");
        }
    }
}