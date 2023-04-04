using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.API;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.Tool;

namespace WitchCompany.Toolkit.Editor
{
    public static class EditorTest
    {
        public static int UnitykeyId;
        
        
        [MenuItem("WitchToolkit/Register")]
        public static async void Register()
        {
            var scene = AssetTool.GetSelectedAsset() as SceneAsset;
            var option = new BlockPublishOption
            {
                targetScene = scene,
                theme = BlockTheme.Outdoor
            };

            var blockName = new JLanguageString
            {
                en = "chichi",
                kr = "치치"
            };

            var response = await WitchAPI.RegisterBlock(option, blockName);
            
            Debug.Log("블록 아이디" + response.blockId);
        }
    }
}