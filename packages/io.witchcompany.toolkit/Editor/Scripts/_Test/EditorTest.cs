using Unity.Plastic.Newtonsoft.Json;
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
        [MenuItem("WitchToolkit/UnityKeyList Test")]
        public static async void Test()
        {
            var response = await WitchAPI.GetUnityKeyList();

            foreach (var key in response.unityKeyList)
            {
                Debug.Log(JsonConvert.SerializeObject(key, Formatting.Indented));
            }
        }
        [MenuItem("WitchToolkit/ViewAuth")]
        public static void GetAuth()
        {
            Debug.Log(AuthConfig.Auth.accessToken);
        }
    }
}