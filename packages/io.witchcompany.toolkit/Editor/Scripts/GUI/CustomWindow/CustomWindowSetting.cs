using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.Configs.Enum;

namespace WitchCompany.Toolkit.Editor
{
    public static class CustomWindowSetting
    {
        private static string _url = "https://docs.unity3d.com/kr/2021.3/Manual/UIE-HowTo-CreateEditorWindow.html";
        private static bool isDebug = true;
        private static bool isDev = true;
        private static bool[] debugOptions = new[] { true, true };
        
        // - 개발자 모드 선택 시 Debug 옵션 선택 가능
        // - Debug On/Off
        // - 버전 표시 - SDK 패키지, 에디터 버전 등
        // - Docu open 버튼
        // - 로그 수준 : None, API, Validation, Tool 중복 선택 가능
        // - 유효성 검사 수준 : Essentials, Strict 두 옵션 중 택1
        
        public static void ShowSetting()
        {
            // EditorGUILayout.LabelField("참고 문서");
            // if (!string.IsNullOrEmpty(_url))
            // {
            //     if (GUILayout.Button(Texture2D.whiteTexture, GUILayout.Width(50), GUILayout.Height(20)))
            //     {
            //         Application.OpenURL(_url);
            //     }
            // }
            //
            // // 개발자 모드 
            // isDev = EditorGUILayout.Toggle("개발자 모드", isDev);
            // EditorGUILayout.BeginVertical("box");
            // // isDev = EditorGUILayout.BeginToggleGroup("개발자 모드", isDev);
            // // 개발자 모드 옵션 on
            // if (isDev)
            // {
            //     isDebug = EditorGUILayout.BeginToggleGroup("Debug", isDebug);
            //     // Debug 옵션 on
            //     // if (isDebug)
            //     // {
            //     debugOptions[0] = EditorGUILayout.Toggle("Validation", debugOptions[0]);
            //     debugOptions[1] = EditorGUILayout.Toggle("API", debugOptions[1]);
            //         
            //     // }
            //
            //     EditorGUILayout.EndToggleGroup();
            // }
            // EditorGUILayout.EndVertical();
            //
            // // 버전 - 값 찾아봐야 함..
            // GUILayout.Space(4);
            // GUILayout.Label("Version");
            // EditorGUILayout.BeginVertical("box");
            // EditorGUILayout.LabelField("Unity : " + "");
            // EditorGUILayout.LabelField("Package : " + "");
            //
            // EditorGUILayout.EndVertical();


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
            isDebug = EditorGUILayout.Toggle("Debug", isDebug);
            EditorGUILayout.EndVertical();
            
        }
        
        /// <summary> 에디터, SDK 패키지 버전 표시 </summary>
        private static void DrawVersion()
        {
            EditorGUILayout.LabelField("Version", EditorStyles.boldLabel);
            
            EditorGUILayout.BeginVertical("box");
            
            EditorGUILayout.LabelField("Editor", "");
            EditorGUILayout.LabelField("Package", "");
            
            EditorGUILayout.EndVertical();
            
        }
        
        /// <summary> Document 버튼 - url 연결 </summary>
        private static void DrawDocument()
        {
            EditorGUILayout.LabelField("Document", EditorStyles.boldLabel);
            
            if (GUILayout.Button(Texture2D.normalTexture, GUILayout.Width(50), GUILayout.Height(20)))
            {
                Application.OpenURL(_url);
            }
        }
        /// <summary> 로그 수준 : None, API, Validation, Tool 중복 선택 가능 </summary>
        private static void DrawLogLevel()
        {
            EditorGUILayout.LabelField("Log Level", EditorStyles.boldLabel);
            
            
        }
        
        static string[] validateLevels = new string[] { ValidateLevel.Essentials.ToString(), ValidateLevel.Strict.ToString() };
        /// <summary> 유효성 검사 수준 : Essentials, Strict 두 옵션 중 택 </summary>
        private static void DrawValidateLevel()
        {
            EditorGUILayout.LabelField("Validate Level", EditorStyles.boldLabel);
            // var menu = new GenericMenu();
            // menu.AddItem(new GUIContent("Red"), ToolkitConfig.CurrValidateLevel == ValidateLevel.Essentials, () => ToolkitConfig.CurrValidateLevel = ValidateLevel.Essentials);
            // menu.AddItem(new GUIContent(""), ToolkitConfig.CurrValidateLevel == ValidateLevel.Strict, () => ToolkitConfig.CurrValidateLevel = ValidateLevel.Strict);
            // menu.ShowAsContext();
            
            
        }
    }
}