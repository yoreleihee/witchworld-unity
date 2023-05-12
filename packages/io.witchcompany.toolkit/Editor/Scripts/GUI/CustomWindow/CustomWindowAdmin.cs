using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.API;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.DataStructure.Admin;
using WitchCompany.Toolkit.Editor.Validation;

namespace WitchCompany.Toolkit.Editor.GUI
{
    public static class CustomWindowAdmin
    {
        private static string _thumbnailPath;
        private static string _pathName;
        private static string _pathNameErrorMsg;
        private static JLanguageString _blockName = new ();
        private static BlockType _blockType;

        private static Texture2D thumbnailImage;
        private static List<string> popupUnityKeys = new ();
        public static List<JUnityKey> unityKeys;
        
        public static void ShowAdmin()
        {
            DrawUnityKey();

            GUILayout.Space(10);
            
            DrawBlockConfig().Forget();
        }
        private static bool _isProcessing = false;
        private static void DrawUnityKey()
        {
            GUILayout.Label("Unity Key", EditorStyles.boldLabel);

            using (new EditorGUILayout.HorizontalScope("box"))
            {
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    var unityKeyIndex = EditorGUILayout.Popup("key list", AdminConfig.UnityKeyIndex, popupUnityKeys.ToArray());

                        if (check.changed)
                        {
                            AdminConfig.UnityKeyIndex = unityKeyIndex;
                        }
                    
                }
                

                using (new EditorGUI.DisabledScope(_isProcessing))
                {
                    // unity key list 조회
                    if (GUILayout.Button("Refresh", GUILayout.Width(100)))
                    {
                        _isProcessing = true;
                        
                        GetUnityKey().Forget();
                    }
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
                var report = AdminPublishValidatior.ValidationCheck();
                if (report.errors.Count > 0)
                {
                    var message = "";
                    foreach (var error in report.errors)
                    {
                        message += error.message + "\n";
                    }
                    EditorUtility.DisplayDialog("Witch Creator Toolkit", $"Publish Failed\n\n{message}", "OK");
                }
                else
                {
                    var selectUnityKey = unityKeys[AdminConfig.UnityKeyIndex];
                    var blockData = new JBlockData()
                    {
                        unityKeyId = selectUnityKey.unityKeyId,
                        pathName = AdminConfig.PathName,
                        ownerNickname = AuthConfig.NickName,
                        blockName = new JLanguageString(AdminConfig.BlockNameEn, AdminConfig.BlockNameKr),
                        blockType = AdminConfig.Type.ToString().ToLower()
                    };
                    
                    EditorUtility.DisplayProgressBar("Witch Creator Toolkit", "Uploading from server....", 1.0f);
                    CustomWindow.IsInputDisable = true;
                    var result = await WitchAPI.UploadBlock(blockData);
                    CustomWindow.IsInputDisable = false;
                    EditorUtility.ClearProgressBar();

                    var resultMsg = result > 0 ? AssetBundleConfig.SuccessMsg : result > -2 ? AssetBundleConfig.FailedMsg : AssetBundleConfig.DuplicationMsg;
                    EditorUtility.DisplayDialog("Witch Creator Toolkit", resultMsg, "OK");
                }
                
            }
        }
        /// <summary> 유니티 키 조회 api 호출 </summary>
        private static async UniTaskVoid GetUnityKey()
        {
            unityKeys = await WitchAPI.GetUnityKeys(0, 0);

            if (unityKeys == null)
            {
                EditorUtility.DisplayDialog("Witch Creator Toolkit", "Unity Key를 조회할 수 없습니다", "OK");
                _isProcessing = false;
                return;
            }
            
            // 키 리스트 초기화 및 서버 데이터 반영
            popupUnityKeys.Clear();
                        
            foreach (var key in unityKeys)
                popupUnityKeys.Add($"{key.pathName} (made by {key.creatorNickName})");

            AdminConfig.UnityKeyIndex = 0;
            _isProcessing = false;
        }
    }
}