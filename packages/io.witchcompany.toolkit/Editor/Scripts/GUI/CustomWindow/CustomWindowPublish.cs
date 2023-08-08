using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using WitchCompany.Toolkit.Editor.API;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.Tool;
using WitchCompany.Toolkit.Module;

namespace WitchCompany.Toolkit.Editor.GUI
{
    public static class CustomWindowPublish
    {
        private static BuildTargetGroup blockPlatform;
        private static JBuildReport buildReport;
        private static string[] bundleTypes =
        {
            AssetBundleConfig.Standalone,
            AssetBundleConfig.Webgl,
            AssetBundleConfig.WebglMobile,
            // AssetBundleConfig.Android,
            // AssetBundleConfig.Ios,
            // AssetBundleConfig.Vr
        };

        private static PlatformType[] platformTypes =
        {
            PlatformType.Standalone,
            PlatformType.Webgl,
            PlatformType.WebglMobile,
            PlatformType.Android,
            PlatformType.Ios,
            PlatformType.Vr,
        };
        
        public static void ShowPublish()
        {
            // 빌드 정보
            DrawPublish();
            
            GUILayout.Space(10);
            
            if (GUILayout.Button("Publish"))
            {
                OnClickPublish().Forget();
            }

            if (buildReport != null)
                DrawReport();
        }

        /// <summary>
        /// 빌드 설정
        /// </summary>
        private static void DrawPublish()
        {
            GUILayout.Label("Publish", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            
            using (new EditorGUILayout.HorizontalScope())
            {
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    var blockScene = EditorGUILayout.ObjectField("Scene", PublishConfig.Scene, typeof(SceneAsset), false) as SceneAsset;
                    if (check.changed)
                        PublishConfig.Scene = blockScene;

                    if (GUILayout.Button("Active Scene", GUILayout.Width(100)))
                    {
                        var activeScenePath = SceneManager.GetActiveScene().path;
                        PublishConfig.Scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(activeScenePath);
                    }
                }
            }

            using (new GUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("Thumbnail", PublishConfig.ThumbnailPath, EditorStyles.textField);
                if (GUILayout.Button("Select", GUILayout.Width(100)))
                {
                    PublishConfig.ThumbnailPath = EditorUtility.OpenFilePanel("Witch Creator Toolkit", "", "jpg");
                    
                }
            } 
            
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                var blockTheme = (BundleTheme)EditorGUILayout.EnumPopup("Theme", PublishConfig.Theme);

                if (check.changed)
                    PublishConfig.Theme = blockTheme;
            } 
            
            // 게임 블록 난이도
            if (PublishConfig.Theme == BundleTheme.Game)
            {
                using var check = new EditorGUI.ChangeCheckScope();
                var blockLevel = (GameLevel)EditorGUILayout.EnumPopup("Level", PublishConfig.Level);
            
                if (check.changed)
                    PublishConfig.Level = blockLevel;
            }
            
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                var capacity = EditorGUILayout.IntField("Capacity (Max 20)", PublishConfig.Capacity);
    
                if (check.changed)
                    PublishConfig.Capacity = capacity <= 20 ? capacity : 20;
            }

            EditorGUILayout.EndVertical();
        }

        private static async UniTaskVoid OnClickPublish()
        {
            // 업로드 권한 확인
            var permission = await WitchAPI.CheckPermission();
                
            if (permission < 0)
            {
                var permissionMsg = permission > -2 ? AssetBundleConfig.AuthMsg : AssetBundleConfig.PermissionMsg;
                EditorUtility.DisplayDialog("Witch Creator Toolkit", permissionMsg, "OK");
                return;
            }
            // 썸네일 확인
            if (string.IsNullOrEmpty(PublishConfig.ThumbnailPath))
            {
                EditorUtility.DisplayDialog("Witch Creator Toolkit", AssetBundleConfig.ThumbnailMsg, "OK");
                return;
            }
            
            // 입력 제한
            CustomWindow.IsInputDisable = true;
            // 번들 추출
            buildReport = WitchToolkitPipeline.PublishWithValidation(GetOption());
                
            if (buildReport.result == JBuildReport.Result.Success)
            {
                // 업로드
                EditorUtility.DisplayProgressBar("Witch Creator Toolkit", "Uploading to server...", 1.0f);
                    
                var result = await UploadBundle();
                var resultMsg = result > 0 ? AssetBundleConfig.SuccessMsg : result > -2 ? AssetBundleConfig.FailedPublishMsg : AssetBundleConfig.DuplicationPublishMsg;
                    
                EditorUtility.DisplayDialog("Witch Creator Toolkit", resultMsg, "OK");
                EditorUtility.ClearProgressBar();
            }   
            // 입력 제한 해제
            CustomWindow.IsInputDisable = false;
        }
        
        private static async UniTask<int> UploadBundle()
        {
            var option = GetOption();
            var rankingKey = GetRankingKey();
            
            var bundleInfos = new List<JBundleInfo>();
            foreach (var bundleType in bundleTypes)
            {
                var bundleInfo = new JBundleInfo();
                var manifestPath = Path.Combine(AssetBundleConfig.BundleExportPath, bundleType, option.BundleKey);
                var crc = AssetBundleTool.ReadManifest(manifestPath);
                if (crc != null)
                {
                    bundleInfo.bundleType = bundleType;
                    bundleInfo.unityVersion = ToolkitConfig.UnityVersion;
                    bundleInfo.toolkitVersion = ToolkitConfig.WitchToolkitVersion;
                    bundleInfo.crc = crc;
                }
                bundleInfos.Add(bundleInfo);
            }
            
            
            var response = await WitchAPI.UploadBundle(option, bundleInfos, rankingKey);
            
            DeleteBundleFile(option);
            
            return response;
        }
        
        public static BlockPublishOption GetOption()
        {
            return new BlockPublishOption
            {
                targetScene = PublishConfig.Scene,
                theme = PublishConfig.Theme,
            };
        }

        private static JRankingKey GetRankingKey()
        {
            // 테마가 게임이 아닐 경우
            if (PublishConfig.Theme != BundleTheme.Game) return null;
            
            // 랭킹 키값 설정
            var dataManager = GameObject.FindObjectOfType<WitchDataManager>(true);
            
            // 데이터 매니저 없으면 랭킹 키값 확인 안함
            if (dataManager == null) return null;
            
            var rankingKey = dataManager.RankingKeys[0];
            return new JRankingKey
            {
                level = PublishConfig.Level.ToString().ToLower(),
                key = rankingKey.key,
                sortType = rankingKey.alignment.ToString().ToLower(),
                dataType = rankingKey.dataType.ToString().ToLower()
            };
            
        }

        private static void DeleteBundleFile(BlockPublishOption option)
        {
            // 번들 파일 삭제
            foreach (var type in bundleTypes)
            {
                var bundlePath = Path.Combine(AssetBundleConfig.BundleExportPath, type, option.BundleKey);
                var manifestPath = bundlePath + ".manifest";
                
                var typePath = Path.Combine(AssetBundleConfig.BundleExportPath, type, type);
                var typeManifestPath = typePath + ".manifest";
                
                File.Delete(bundlePath);
                File.Delete(manifestPath);
                
                File.Delete(typePath);
                File.Delete(typeManifestPath);
            }
        }

        /// <summary>
        /// 빌드 결과
        /// - 방금 빌드 결과(성공/취소/실패)
        /// - 빌드파일 아웃풋 경로 (./WitchToolkit/Bundles)
        /// - 최종 빌드파일 용량
        /// - 빌드 종료 시간
        /// </summary>
        private static void DrawReport()
        {
            GUILayout.Label("Bundle Report", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            
            EditorGUILayout.LabelField("Result", buildReport.result.ToString());
            
            if (buildReport.result == JBuildReport.Result.Success)
            {
                // TODO: 플랫폼에 따라 분류
                //EditorGUILayout.LabelField("ExportPath", buildReport.exportPath);
                //EditorGUILayout.LabelField("TotalSize", $"{CommonTool.ByteToMb(buildReport.totalSizeByte, 2)} MB");
                // 시작시간
                EditorGUILayout.LabelField("StartTime", $"{buildReport.BuildStatedAt.ToString()}");
                // 종료시간
                EditorGUILayout.LabelField("EndTime", buildReport.BuildEndedAt.ToString());
                // 소요시간 
                var time = buildReport.BuildEndedAt - buildReport.BuildStatedAt;
                EditorGUILayout.LabelField("Duration", (int)time.TotalSeconds + "s");;

            }
            EditorGUILayout.EndVertical();
        }
    }
}