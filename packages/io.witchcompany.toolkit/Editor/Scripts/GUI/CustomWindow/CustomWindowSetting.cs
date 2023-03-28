using UnityEditor;
using UnityEngine;

namespace WitchCompany.Toolkit.Editor
{
    public static class CustomWindowSetting
    {
        private static string _url = "https://docs.unity3d.com/kr/2021.3/Manual/UIE-HowTo-CreateEditorWindow.html";
        private static bool isDebug = true;
        private static bool isDev = true;
        private static bool[] debugOptions = new[] { true, true };
        
        public static void ShowSetting()
        {
            EditorGUILayout.LabelField("Setting");
            GUILayout.Space(4);
            EditorGUILayout.LabelField("참고 문서");
            if (!string.IsNullOrEmpty(_url))
            {
                if (GUILayout.Button(Texture2D.whiteTexture, GUILayout.Width(50), GUILayout.Height(20)))
                {
                    Application.OpenURL(_url);
                }
            }

            // 개발자 모드 
            isDev = EditorGUILayout.Toggle("개발자 모드", isDev);
            EditorGUILayout.BeginVertical("box");
            // isDev = EditorGUILayout.BeginToggleGroup("개발자 모드", isDev);
            // 개발자 모드 옵션 on
            if (isDev)
            {
                isDebug = EditorGUILayout.BeginToggleGroup("Debug", isDebug);
                // Debug 옵션 on
                // if (isDebug)
                // {
                debugOptions[0] = EditorGUILayout.Toggle("Validation", debugOptions[0]);
                debugOptions[1] = EditorGUILayout.Toggle("API", debugOptions[1]);
                    
                // }

                EditorGUILayout.EndToggleGroup();
            }
            EditorGUILayout.EndVertical();

            // 버전 - 값 찾아봐야 함..
            GUILayout.Space(4);
            GUILayout.Label("Version");
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Unity : " + "");
            EditorGUILayout.LabelField("Package : " + "");

            EditorGUILayout.EndVertical();

        }
    }
}