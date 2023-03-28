using System;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.Configs.Enum;

namespace WitchCompany.Toolkit.Editor
{
    public static class KmTest
    {
        private static LogLevel a = LogLevel.None;
        private static LogLevel b = LogLevel.Validation;
        private static LogLevel c = LogLevel.Tool;
        private static LogLevel d = LogLevel.API;
        private static LogLevel e = LogLevel.Tool | LogLevel.Validation | LogLevel.API;

        [MenuItem("WitchToolkit/Test2")]
        public static void Test2()
        {
            Debug.Log(ToolkitConfig.UnityVersion);

            //ToolkitConfig.CurrLogLevel = LogLevel.Validation | LogLevel.API;
        }
        
        private static void Log(LogLevel target)
        {
            var i = (int) target;
            Debug.Log($"{target} -> {Convert.ToString(i, 2)} -> {(LogLevel)i}");
        }
    }
}