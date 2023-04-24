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
        [MenuItem("WitchToolkit/View Auth")]
        public static void GetAuth()
        {
            Debug.Log(AuthConfig.Auth.accessToken);
        }
    }
}