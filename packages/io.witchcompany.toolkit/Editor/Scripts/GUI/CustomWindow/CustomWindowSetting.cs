using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;

namespace WitchCompany.Toolkit.Editor.GUI
{
    public static class CustomWindowSetting
    {

        // - 개발자 모드 선택 시 Debug 옵션 선택 가능
        // - Debug On/Off
        // - 버전 표시 - SDK 패키지, 에디터 버전 등
        // - Docu open 버튼
        // - 로그 수준 : None, API, Validation, Tool 중복 선택 가능
        // - 유효성 검사 수준 : Essentials, Strict 두 옵션 중 택1
        
        public static void ShowSetting()
        {
            DrawDevMode();
            EditorGUILayout.Space(10);
            DrawVersion();
            EditorGUILayout.Space(10);
            DrawDocument();
            EditorGUILayout.Space(10);
            DrawLogLevel();
            EditorGUILayout.Space(10);
            DrawValidateLevel();
        }
        
        /// <summary> 개발자 모드 - Debug on/off </summary>
        private static void DrawDevMode()
        {
            EditorGUILayout.LabelField("Developer Mode", EditorStyles.boldLabel);
            
            EditorGUILayout.BeginVertical("box");
            ToolkitConfig.DeveloperMode = EditorGUILayout.Toggle("Debug", ToolkitConfig.DeveloperMode);
            EditorGUILayout.EndVertical();
            
        }
        
        /// <summary> 에디터, SDK 패키지 버전 표시 </summary>
        private static void DrawVersion()
        {
            EditorGUILayout.LabelField("Version", EditorStyles.boldLabel);
            
            EditorGUILayout.BeginVertical("box");
            
            EditorGUILayout.LabelField("Editor", ToolkitConfig.UnityVersion);
            EditorGUILayout.LabelField("Package", ToolkitConfig.WitchToolkitVersion);
            
            EditorGUILayout.EndVertical();
            
        }
        
        /// <summary> Document 버튼 - url 연결 </summary>
        private static void DrawDocument()
        {
            EditorGUILayout.LabelField("Document", EditorStyles.boldLabel);
            
            if (GUILayout.Button("README", GUILayout.Width(100), GUILayout.Height(20)))
            {
                Application.OpenURL(ToolkitConfig.WitchToolkitDocumentUrl);
            }
        }
        /// <summary> 로그 수준 : None, API, Validation, Tool 중복 선택 가능 </summary>
        private static void DrawLogLevel()
        {
            EditorGUILayout.LabelField("Log Level", EditorStyles.boldLabel);


            using var check = new EditorGUI.ChangeCheckScope();
            var logLevel = (LogLevel)EditorGUILayout.EnumFlagsField("", ToolkitConfig.CurrLogLevel);
                
            if (check.changed)
                ToolkitConfig.CurrLogLevel = logLevel;
        }
        
        /// <summary> 유효성 검사 수준 : Essentials, Strict 두 옵션 중 택 </summary>
        private static void DrawValidateLevel()
        {
            EditorGUILayout.LabelField("Validate Level", EditorStyles.boldLabel);
                
            using var check = new EditorGUI.ChangeCheckScope();
            var validateLevel = (ValidateLevel)EditorGUILayout.EnumPopup("", ToolkitConfig.CurrValidateLevel);

            if (check.changed)
                ToolkitConfig.CurrValidateLevel = validateLevel;
        }
    }
}