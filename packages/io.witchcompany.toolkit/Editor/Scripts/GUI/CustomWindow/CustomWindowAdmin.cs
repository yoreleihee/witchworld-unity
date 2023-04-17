using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.Validation;

namespace WitchCompany.Toolkit.Editor.GUI
{
    public static class CustomWindowAdmin
    {
        private const int maxBlockNameLength = 20;
        
        private static string _thumbnailPath;
        private static string _pathName;
        private static string _pathNameErrorMsg;
        private static JLanguageString _blockName = new JLanguageString();
        private static BlockType _blockType;
        private static int _selectedUnityKey = 0;

        private static Texture2D thumbnailImage;
        // todo : 유니티 키 리스트 api에서 받은 값 저장
        private static List<string> unityKeyList = new List<string>() { "ryu(made by jj)", "ryu(made by hh)", "ryu(made by kk)"};
        
        public static void ShowAdmin()
        {
            
            DrawUnityKey();

            GUILayout.Space(10);
            
            DrawBlockConfig();
            
        }

        private static async UniTaskVoid DrawUnityKey()
        {
            GUILayout.Label("Unity Key", EditorStyles.boldLabel);

            // 키 선택
            using (new EditorGUILayout.HorizontalScope("box"))
            {
                // todo : unity key 페이징 조회 api와 연동
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    var unityKeyIndex = EditorGUILayout.Popup("key list", AdminConfig.UnityKeyIndex, unityKeyList.ToArray());

                    if (check.changed)
                        AdminConfig.UnityKeyIndex = unityKeyIndex;
                }
                
                
                if (GUILayout.Button("Refresh", GUILayout.Width(100)))
                {
                    CustomWindow.IsInputDisable = true;
                    
                    EditorUtility.DisplayProgressBar("Witch Creator Toolkit", "Getting unity key list from server....", 1.0f);
                    await UniTask.Delay(3000);
                    EditorUtility.ClearProgressBar();
                    
                    CustomWindow.IsInputDisable = false;
                    AdminConfig.UnityKeyIndex = 0;
                }
            }
            
        }
        
        private static async UniTaskVoid DrawBlockConfig()
        {
            GUILayout.Label("Block Config", EditorStyles.boldLabel);

            using (new EditorGUILayout.VerticalScope("box"))
            {
                using (new GUILayout.HorizontalScope())
                {
                    // todo : 썸네일 미리보기
                    EditorGUILayout.LabelField("thumbnail", AdminConfig.ThumbnailPath, EditorStyles.textField);
                    if (GUILayout.Button("Select", GUILayout.Width(100)))
                    {
                        AdminConfig.ThumbnailPath = EditorUtility.OpenFilePanel("Witch Creator Toolkit", "", "jpg");
                    
                    }
                } 

                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    // 정규식에 맞지 않을 경우 이전 값으로 되돌림
                    var prePathName = _pathName;
                    _pathName = EditorGUILayout.TextField("path name", AdminConfig.PathName);

                    if (check.changed)
                        AdminConfig.PathName = _pathName;
                }

                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    // 최대 글자수 넘으면 이전값으로 되돌림
                    _blockName.kr = EditorGUILayout.TextField("block name (한글)", AdminConfig.BlockNameKr);

                    if (check.changed)
                        AdminConfig.BlockNameKr = _blockName.kr;
                }

                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    _blockName.en = EditorGUILayout.TextField("block name (영문)", AdminConfig.BlockNameEn);
                    
                    if (check.changed)
                        AdminConfig.BlockNameEn = _blockName.en; 
                }

                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    var blockType = (BlockType)EditorGUILayout.EnumPopup("type", AdminConfig.Type);

                    if (check.changed)
                        AdminConfig.Type = blockType;
                } 
            }

            if (GUILayout.Button("Publish"))
            {
                CustomWindow.IsInputDisable = true;

                var report = AdminPublishValidatior.ValidationCheck();
                if (report.errors.Count > 0)
                {
                    var message = "";
                    foreach (var error in report.errors)
                    {
                        message += error.message + "\n";
                        Debug.Log(error.message);
                    }
                    EditorUtility.DisplayDialog("Publish Failed", message, "OK");
                }
                else
                {
                    // todo : 블록 생성 api 연동
                    EditorUtility.DisplayProgressBar("Witch Creator Toolkit", "Uploading from server....", 1.0f);
                    await UniTask.Delay(5000);
                    EditorUtility.ClearProgressBar();
                    
                    // todo : 유니티 키 생성 api 결과에 따라 팝업창 메시지 다르게 변경할 것
                    // EditorUtility.DisplayDialog("Witch Creator Toolkit", successMsg, "OK");
                    EditorUtility.DisplayDialog("Witch Creator Toolkit", "블록 생성 성공", "OK");
                }
                
                CustomWindow.IsInputDisable = false;
            }
        }
    }
}