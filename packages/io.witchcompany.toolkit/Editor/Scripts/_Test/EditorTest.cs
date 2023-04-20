using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.API;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.Tool;
using Formatting = Newtonsoft.Json.Formatting;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace WitchCompany.Toolkit.Editor
{
    public static class EditorTest
    {
        [MenuItem("WitchToolkit/API Test")]
        public static async void Test()
        {
            var response = await WitchAPI.GetBlock("test3");

            Debug.Log(JsonConvert.SerializeObject(response, Formatting.Indented));
        }
        [MenuItem("WitchToolkit/ViewAuth")]
        public static void GetAuth()
        {
            Debug.Log(AuthConfig.Auth.accessToken);
        }
    }
}