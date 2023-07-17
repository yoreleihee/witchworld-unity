using System.Collections.Generic;
using System.Text;
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
using WitchCompany.Toolkit.Validation;

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

                // 게임 블록 난이도
                if (AdminConfig.BlockTheme == BlockTheme.Game)
                {
                    // 난이도
                    using var check = new EditorGUI.ChangeCheckScope();
                    var blockLevel = (BlockLevel)EditorGUILayout.EnumPopup("level", AdminConfig.BlockLevel);

                    if (check.changed)
                        AdminConfig.BlockLevel = blockLevel;
                }
                
                // 블록 조회 상태
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    var blockStatus = (BlockStatus)EditorGUILayout.EnumPopup("status", AdminConfig.BlockStatus);

                    if (check.changed)
                        AdminConfig.BlockStatus = blockStatus;
                }

                // 프라이빗 블록 여부
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    var blockDesc = EditorGUILayout.Toggle("private block option", AdminConfig.IsPrivate);
                    
                    if (check.changed)
                        AdminConfig.IsPrivate = blockDesc;
                }
                
                // 프라이빗 블록 itemCA
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
                var report = AdminPublishValidator.ValidationCheck();
                await DrawDisplayDialog(report, true);
            }

            if (GUILayout.Button("Update"))
            {
                var report = AdminPublishValidator.ValidationUpdateCheck();
                await DrawDisplayDialog(report, false);
            }
        }

        private static async UniTask DrawDisplayDialog(ValidationReport report, bool isPublish)
        {
            if (report.errors.Count > 0)
            {
                var message = new StringBuilder();
                foreach (var error in report.errors)
                    message.Append(error.message + "\n");
                
                var alertMsg = isPublish ? AssetBundleConfig.FailedPublishMsg : AssetBundleConfig.FailedUpdateMsg;
                EditorUtility.DisplayDialog("Witch Creator Toolkit", $"{alertMsg}\n\n{message}", "OK");
            }
            else
            {
                var result = true;
                if (isPublish)
                    result = EditorUtility.DisplayDialog("Witch Creator Toolkit", RankingAlertMsg(), "OK");
                
                if (result)
                {
                    EditorUtility.DisplayProgressBar("Witch Creator Toolkit", "Uploading from server...", 1.0f);
                    CustomWindow.IsInputDisable = true;
                            
                    var alertMsg = isPublish ? await OnPublish() : await OnUpdate();
                            
                    CustomWindow.IsInputDisable = false;
                    EditorUtility.ClearProgressBar();
                    EditorUtility.DisplayDialog("Witch Creator Toolkit", alertMsg, "OK");
                }
            }
        }

        private static async UniTask<string> OnPublish()
        {
            // 블록 업로드
            var blockData = CreateBlockData(true);
            var resultBlockId = await WitchAPI.UploadBlock(blockData);
            var resultMsg = resultBlockId > -2 ? AssetBundleConfig.FailedPublishMsg : AssetBundleConfig.DuplicationPublishMsg;
            
            if (resultBlockId < 0) return resultMsg;

            // 랭킹 키값 설정
            var resultKey = await UploadRankingKeys(resultBlockId);
            
            return resultKey ? AssetBundleConfig.SuccessMsg : AssetBundleConfig.FailedKeyMsg;
        }

        private static async UniTask<string> OnUpdate()
        {
            var blockData = CreateBlockData(false);
            var result = await WitchAPI.UpdateBlockData(blockData);

            return result ? AssetBundleConfig.SuccessUpdateMsg : AssetBundleConfig.FailedUpdateMsg;
        }

        private static JBlockData CreateBlockData(bool isPublish)
        {
            var blockData = new JBlockData()
            {
                pathName = AdminConfig.PathName,
                blockName = new JLanguageString(AdminConfig.BlockNameEn, AdminConfig.BlockNameKr),
                blockTheme = AdminConfig.BlockTheme.ToString().ToLower(),
                blockLevel = AdminConfig.BlockTheme != BlockTheme.Game ? null : AdminConfig.BlockLevel.ToString().ToLower(),
                blockDescription = new JLanguageString(AdminConfig.BlockDescriptionEn, AdminConfig.BlockDescriptionKr),
                blockStatus = (int)AdminConfig.BlockStatus,
                isPrivate = AdminConfig.IsPrivate,
                itemCA = AdminConfig.IsPrivate ? AdminConfig.ItemCA : ""
            };

            Debug.Log("업데이트 블록 정보\n" + JsonConvert.SerializeObject(blockData));
            if (!isPublish) return blockData;

            blockData.unityKeyId = unityKeys[AdminConfig.UnityKeyIndex].unityKeyId;
            blockData.blockType = AdminConfig.BlockType.ToString().ToLower();
            blockData.ownerNickname = AuthConfig.NickName;
            Debug.Log("업로드 블록 정보\n" + JsonConvert.SerializeObject(blockData));

            
            return blockData;
        }

        private static async UniTask<bool> UploadRankingKeys(int blockId)
        {
            // 랭킹 키값 설정
            var result = true;
            var dataManager = GameObject.FindObjectOfType<WitchDataManager>(true);

            // 데이터 매니저 없으면 랭킹 키값 확인 안함
            if (dataManager != null)
            {
                if (dataManager.RankingKeys.Count > 0)
                {
                    var keys = new List<JRankingKey>();
                    foreach (var keyGroup in dataManager.RankingKeys)
                    {
                        var data = new JRankingKey()
                        {
                            rankingKey = keyGroup.key,
                            rankingKeyType = keyGroup.alignment.ToString().ToLower(),
                            rankingKeyDataType = keyGroup.dataType.ToString().ToLower()
                        };
                        keys.Add(data);
                    }
                    result = await WitchAPI.SetRankingKeys(blockId, keys);
                }
            }
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

        private static string RankingAlertMsg()
        {
            var dataManager = AdminPublishValidator.ValidateDataManager();
            var keyStr = new StringBuilder();
            var message =  "랭킹보드를 사용하지 않습니다. 진행하시겠습니까?";

            if (dataManager)
            {
                if (dataManager.RankingKeys.Count <= 0)
                {
                    message = "랭킹보드에 지정된 키가 없습니다. 진행하시겠습니까?";
                }
                else
                {
                    var keys = new List<string>();
                    foreach (var keyGroup in dataManager.RankingKeys)
                    {
                        keys.Add(keyGroup.key);
                    }
                    keyStr.AppendJoin(", ", keys);
                    message = $"랭킹보드에 [{keyStr}]를 사용합니다. 진행하시겠습니까?";
                }
            }

            return message;
        }

        
    }
}