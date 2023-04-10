using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using WitchCompany.Toolkit.Editor.API;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.Tool;

namespace WitchCompany.Toolkit.Editor
{
    public static class KmTest
    { 
        // [MenuItem("WitchToolkit/Permission")]
        // public static async void Login()
        // {
        //     var json = await WitchAPI.Login("ohju96@naver.com", "12DHwngus!!");
        //     AuthConfig.Auth = json;
        //     
        //     Debug.Log(JsonConvert.SerializeObject(json));
        //     Debug.Log(CommonTool.TimeStampToDateTime(json.accessExpire).ToString("u"));
        //     Debug.Log(CommonTool.TimeStampToDateTime(json.refreshExpire).ToString("u"));
        // }
        //
        // [MenuItem("WitchToolkit/SaveAndClearFlags")]
        // public static void Release()
        // {
        //     StaticRevertTool.SaveAndClearFlags();
        //     EditorSceneManager.SaveOpenScenes();
        // }
        //
        // [MenuItem("WitchToolkit/RevertFlags")]
        // public static void Revert()
        // {
        //     StaticRevertTool.RevertFlags();
        //     EditorSceneManager.SaveOpenScenes();
        // }
        
        // var scn = AssetTool.GetSelectedAsset() as SceneAsset;
        // var option = new BlockPublishOption
        // {
        //     targetScene = scn,
        //     theme = BlockTheme.Outdoor
        // };
        //
        // var manifest = new JManifest
        // {
        //     unityVersion = ToolkitConfig.UnityVersion,
        //     toolkitVersion = ToolkitConfig.WitchToolkitVersion,
        //     crc = "1017531261",
        // };
        //
        // await WitchAPI.UploadBlock(option, manifest);
        //
        // // [MenuItem("WitchToolkit/Test_GetUserInfo")]
        // public static async void GetUserInfo()
        // {
        //     var auth = AuthConfig.Auth;
        //     Debug.Log("처음 AUTH---------------");
        //     Debug.Log(JsonConvert.SerializeObject(auth));
        //     Debug.Log(CommonTool.TimeStampToDateTime(auth.accessExpire).ToString("u"));
        //     Debug.Log(CommonTool.TimeStampToDateTime(auth.refreshExpire).ToString("u"));
        //     
        //     Debug.Log("유저 정보 요청---------------");
        //     var json = await WitchAPI.GetUserInfo();
        //     
        //     Debug.Log("다음 AUTH---------------");
        //     auth = AuthConfig.Auth;
        //     Debug.Log(JsonConvert.SerializeObject(auth));
        //     Debug.Log(CommonTool.TimeStampToDateTime(auth.accessExpire).ToString("u"));
        //     Debug.Log(CommonTool.TimeStampToDateTime(auth.refreshExpire).ToString("u"));
        // }
        //
        // // [MenuItem("WitchToolkit/Test_BuildBundle")]
        // public static async void BuildBundle()
        // {
        //     var option = AssetTool.GetSelectedAsset() as BlockPublishOption;
        //     WitchToolkitPipeline.PublishWithValidation(option);
        //     
        // }
        //
        // // [MenuItem("WitchToolkit/Test_KM")]
        // public static async void Test_KM()
        // {
        //     var option = AssetTool.GetSelectedAsset() as BlockPublishOption;
        // }
    }
}