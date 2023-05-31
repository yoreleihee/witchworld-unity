using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.API;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.DataStructure.Admin;
using WitchCompany.Toolkit.Editor.Validation;
using WitchCompany.Toolkit.Module;

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
                    // 썸네일
                    EditorGUILayout.LabelField("thumbnail", AdminConfig.ThumbnailPath, EditorStyles.textField);
                    if (GUILayout.Button("Select", GUILayout.Width(100)))
                    {
                        AdminConfig.ThumbnailPath = EditorUtility.OpenFilePanel("Witch Creator Toolkit", "", "jpg");
                    }
                } 
                // pathName
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    // 정규식에 맞지 않을 경우 이전 값으로 되돌림
                    _pathName = EditorGUILayout.TextField("path name", AdminConfig.PathName);

                    if (check.changed)
                        AdminConfig.PathName = _pathName;
                }
                // 블록 이름(한글)
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    // 최대 글자수 넘으면 이전값으로 되돌림
                    _blockName.kr = EditorGUILayout.TextField("block name (한글)", AdminConfig.BlockNameKr);

                    if (check.changed)
                        AdminConfig.BlockNameKr = _blockName.kr;
                }
                // 블록 이름(영문)
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    _blockName.en = EditorGUILayout.TextField("block name (영문)", AdminConfig.BlockNameEn);
                    
                    if (check.changed)
                        AdminConfig.BlockNameEn = _blockName.en; 
                }
                // 설명(한글)
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    var blockDesc = EditorGUILayout.TextField("description (한글)", AdminConfig.BlockDescriptionKr);

                    if (check.changed)
                        AdminConfig.BlockDescriptionKr = blockDesc;
                }
                // 설명(영문)
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    var blockDesc = EditorGUILayout.TextField("description (영문)", AdminConfig.BlockDescriptionEn);

                    if (check.changed)
                        AdminConfig.BlockDescriptionEn = blockDesc;
                }
                // 테마
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    var blockTheme = (BlockTheme)EditorGUILayout.EnumPopup("theme", AdminConfig.BlockTheme);

                    if (check.changed)
                        AdminConfig.BlockTheme = blockTheme;
                }

                if (AdminConfig.BlockTheme == BlockTheme.Game)
                {
                    // 난이도
                    using var check = new EditorGUI.ChangeCheckScope();
                    var blockLevel = (BlockLevel)EditorGUILayout.EnumPopup("level", AdminConfig.BlockLevel);

                    if (check.changed)
                        AdminConfig.BlockLevel = blockLevel;
                }

                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    var blockDesc = EditorGUILayout.Toggle("private block option", AdminConfig.IsPrivate);
                    
                    if (check.changed)
                        AdminConfig.IsPrivate = blockDesc;
                }
                
                if (AdminConfig.IsPrivate)
                {
                    using var check = new EditorGUI.ChangeCheckScope();
                    var itemCA = EditorGUILayout.TextField("itemCA", AdminConfig.ItemCA);

                    if (check.changed)
                        AdminConfig.ItemCA = itemCA;
                }
            }

            if (GUILayout.Button("Publish"))
            {
                var report = AdminPublishValidatior.ValidationCheck();
                if (report.errors.Count > 0)
                {
                    var message = "";
                    foreach (var error in report.errors)
                        message += error.message + "\n";
                    
                    EditorUtility.DisplayDialog("Witch Creator Toolkit", $"Publish Failed\n\n{message}", "OK");
                }
                else
                {
                    EditorUtility.DisplayProgressBar("Witch Creator Toolkit", "Uploading from server....", 1.0f);
                    CustomWindow.IsInputDisable = true;
                    
                    var resultMsg = await OnPublish();
                    
                    CustomWindow.IsInputDisable = false;
                    EditorUtility.ClearProgressBar();
                    EditorUtility.DisplayDialog("Witch Creator Toolkit", resultMsg, "OK");
                }
            }
        }

        private static async UniTask<string> OnPublish()
        {
            // 블록 업로드
            var resultBlock = await UploadBlock();
            var resultMsg = resultBlock > -2 ? AssetBundleConfig.FailedMsg : AssetBundleConfig.DuplicationMsg;
            
            if (resultBlock < 0) return resultMsg;
            
            // 랭킹 키값 설정
            var dataManager = GameObject.FindObjectOfType<WitchDataManager>(true);

            // 데이터 매니저 없으면 랭킹 키값 확인 안함
            if (dataManager == null)
                return AssetBundleConfig.SuccessMsg;
                
            var resultKey = false;
            if (dataManager.RankingKeys.Count > 0)
            {
                var list = new List<JRankingData>();
                foreach (var keyGroup in dataManager.RankingKeys)
                {
                    var data = new JRankingData()
                    {
                        rankingKey = keyGroup.key,
                        rankingKeyType = keyGroup.alignment.ToString().ToLower()
                    };
                    list.Add(data);
                }
                resultKey = await SetRankingKeys(resultBlock, list);
            }
            
            return resultKey ? AssetBundleConfig.SuccessMsg : AssetBundleConfig.FailedKeyMsg;
        }

        private static async UniTask<int> UploadBlock()
        {
            var selectedKey = unityKeys[AdminConfig.UnityKeyIndex];
            var blockLevel = AdminConfig.BlockTheme != BlockTheme.Game ? null : AdminConfig.BlockLevel.ToString().ToLower();
            var blockData = new JBlockData()
            {
                unityKeyId = selectedKey.unityKeyId,
                pathName = AdminConfig.PathName,
                ownerNickname = AuthConfig.NickName,
                blockName = new JLanguageString(AdminConfig.BlockNameEn, AdminConfig.BlockNameKr),
                blockType = AdminConfig.BlockType.ToString().ToLower(),
                blockTheme = AdminConfig.BlockTheme.ToString().ToLower(),
                blockLevel = blockLevel,
                blockDescription = new JLanguageString(AdminConfig.BlockDescriptionKr, AdminConfig.BlockDescriptionEn),
                isPrivate = AdminConfig.IsPrivate,
                itemCA = AdminConfig.ItemCA
            };
                
            var result = await WitchAPI.UploadBlock(blockData);

            return result;
        }

        private static async UniTask<bool> SetRankingKeys(int blockId, List<JRankingData> rankingKeys)
        {
            var rankingData = new JRanking
            {
                blockId = blockId,
                rankingKeys = rankingKeys
            };
            Debug.Log(JsonConvert.SerializeObject(rankingData, Formatting.Indented));
            
            var result = await WitchAPI.SetRankingKeys(rankingData);
            return result;
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