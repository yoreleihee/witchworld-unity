using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;

namespace WitchCompany.Toolkit.Editor
{
    public class CustomWindowTest : EditorWindow
    {

        [MenuItem ("WitchToolkit/Witch Control Panel")]
        public static void  ShowControlPanel () {
            EditorWindow wnd = GetWindow(typeof(CustomWindowTest));
            wnd.titleContent = new GUIContent("Witch Control Panel");
            wnd.minSize = new Vector2(640, 480);

        }
        
        int toolbarIdx = 0;
        private readonly GUIContent[] toolbarLabels = new GUIContent[4]
        {
            new GUIContent("Authentication"),
            new GUIContent("Validation"),
            new GUIContent("Publish"),
            new GUIContent("Settings")
        };
        void OnGUI()
        {
            // Tool bar
            toolbarIdx = GUILayout.Toolbar(toolbarIdx, toolbarLabels);
            
            // 선택한 메뉴에 따라 다른 함수 호출
            switch (toolbarIdx)
            {
                case 0 : 
                    CustomWindowAuth.ShowAuth();
                    break;
                case 1 :
                    CustomWindowValidation.ShowValidation();
                    break;
                case 2 : 
                    CustomWindowPublish.ShowPublish();
                    break;
                case 3 : 
                    CustomWindowSetting.ShowSetting();
                    break;
                default: break;
            }
        }



        void ShowValidation()
        {
            EditorGUILayout.LabelField("Validation");
        }
        
        
        // private static string _url = "https://docs.unity3d.com/kr/2021.3/Manual/UIE-HowTo-CreateEditorWindow.html";
        // private bool isDebug = true;
        // private bool isDev = true;
        // private bool[] debugOptions = new[] { true, true };
        //
        // void ShowSetting()
        // {
        //     EditorGUILayout.LabelField("Setting");
        //     GUILayout.Space(4);
        //     EditorGUILayout.LabelField("참고 문서");
        //     if (!string.IsNullOrEmpty(_url))
        //     {
        //         if (GUILayout.Button(Texture2D.whiteTexture, GUILayout.Width(50), GUILayout.Height(20)))
        //         {
        //             Application.OpenURL(_url);
        //         }
        //     }
        //
        //     // 개발자 모드 
        //     isDev = EditorGUILayout.Toggle("개발자 모드", isDev);
        //     EditorGUILayout.BeginVertical("box");
        //     // isDev = EditorGUILayout.BeginToggleGroup("개발자 모드", isDev);
        //     // 개발자 모드 옵션 on
        //     if (isDev)
        //     {
        //         isDebug = EditorGUILayout.BeginToggleGroup("Debug", isDebug);
        //         // Debug 옵션 on
        //         // if (isDebug)
        //         // {
        //             debugOptions[0] = EditorGUILayout.Toggle("Validation", debugOptions[0]);
        //             debugOptions[1] = EditorGUILayout.Toggle("API", debugOptions[1]);
        //             
        //         // }
        //
        //         EditorGUILayout.EndToggleGroup();
        //     }
        //     EditorGUILayout.EndVertical();
        //
        //     // 버전 - 값 찾아봐야 함..
        //     GUILayout.Space(4);
        //     GUILayout.Label("Version");
        //     EditorGUILayout.BeginVertical("box");
        //     EditorGUILayout.LabelField("Unity : " + "");
        //     EditorGUILayout.LabelField("Package : " + "");
        //
        //     EditorGUILayout.EndVertical();
        //
        // }

        
    }
}