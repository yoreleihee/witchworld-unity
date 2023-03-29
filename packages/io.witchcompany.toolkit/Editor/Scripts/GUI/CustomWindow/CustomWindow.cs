using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;

namespace WitchCompany.Toolkit.Editor
{
    public class CustomWindow : EditorWindow
    {
        private int toolbarIdx = 0;
        private int preToolbarIdx = 0;
        

        [MenuItem ("WitchToolkit/Witch Control Panel")]
        public static void  ShowControlPanel () {
            EditorWindow wnd = GetWindow(typeof(CustomWindow));
            wnd.titleContent = new GUIContent("Witch Control Panel");
            wnd.minSize = new Vector2(640, 480);
        }
        
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
            // 이전 툴바 저장
            if (preToolbarIdx != toolbarIdx)
            {
                preToolbarIdx = toolbarIdx;
            }


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
    }
}