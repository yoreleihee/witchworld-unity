using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;

namespace WitchCompany.Toolkit.Editor.GUI
{
    public class CustomWindow : EditorWindow
    {
        Texture bannerImage;
        
        
        [MenuItem ("WitchToolkit/Witch Creator Toolkit")]
        public static void  ShowControlPanel () {
            EditorWindow wnd = GetWindow(typeof(CustomWindow));
            wnd.titleContent = new GUIContent("Witch Creator Toolkit");
            wnd.minSize = new Vector2(518, 600);
            wnd.maxSize = new Vector2(518, 2000);
        }
        
        private readonly GUIContent[] toolbarLabels = new GUIContent[4]
        {
            new GUIContent("Authentication"),
            new GUIContent("Validation"),
            new GUIContent("Publish"),
            new GUIContent("Settings")
        };
        
        private void OnGUI()
        {
            // if (bannerImage == null)
            //     bannerImage = Resources.Load<Texture2D>("image_toolkit_banner");
            // GUILayout.Box(bannerImage);
            ToolkitConfig.CurrControlPanelType = (ControlPanelType)GUILayout.Toolbar((int)ToolkitConfig.CurrControlPanelType, toolbarLabels);
            
            // 선택한 메뉴에 따라 다른 함수 호출
            switch (ToolkitConfig.CurrControlPanelType)
            {
                case ControlPanelType.Auth: 
                    CustomWindowAuth.ShowAuth();
                    break;
                case ControlPanelType.Validate :
                    CustomWindowValidation.ShowValidation();
                    break;
                case ControlPanelType.Publish : 
                    CustomWindowPublish.ShowPublish();
                    break;
                case ControlPanelType.Config : 
                    CustomWindowSetting.ShowSetting();
                    break;
                default: break;
            }

        }
        
        /// <summary> Editor Window가 닫힐 때 호출 </summary>
        private void OnDestroy()
        {
            // input field 값 초기화
            CustomWindowAuth.email = "";
            CustomWindowAuth.password = "";
        }
    }
}