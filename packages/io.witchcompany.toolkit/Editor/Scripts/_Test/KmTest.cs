using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Tool.API;

namespace WitchCompany.Toolkit.Editor
{
    public static class KmTest
    {
        [MenuItem("WitchToolkit/Login")]
        public static async void Login()
        {
            var json = await WitchAPI.Login("kmkim@witchcompany.io", "1998Kimin!");
            Debug.Log(JsonConvert.SerializeObject(json));
        }
        
        [MenuItem("WitchToolkit/Refresh")]
        public static async void Refresh()
        {
            var json = await WitchAPI.Refresh();
            Debug.Log(JsonConvert.SerializeObject(json));
        }
        
        [MenuItem("WitchToolkit/GetUserInfo")]
        public static async void GetUserInfo()
        {
            var json = await WitchAPI.GetUserInfo();
            Debug.Log(JsonConvert.SerializeObject(json));
        }
    }
}