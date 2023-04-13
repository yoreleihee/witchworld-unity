using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using WitchCompany.Toolkit.Editor.API;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.Tool;

namespace WitchCompany.Toolkit.Editor.GUI
{
    public static class CustomWindowPublish
    {
        private static SceneAsset blockScene;
        private static BlockTheme blockTheme;
        private static BuildTargetGroup blockPlatform;
        public static JBuildReport buildReport;
        private static UploadState uploadResult = UploadState.None;
        
        private const string successMsg = "Upload Result : Success\n블록을 서버에 업로드했습니다";
        private const string failedMsg = "Upload Result : Failed\n블록을 서버에 업로드하지 못했습니다\n다시 시도해주세요";
        
        private enum UploadState
        {
            None,
            Uploading,
            Success,
            Failed
        }

        public static void ShowPublish()
        {
            // 빌드 정보
            DrawPublish();
            
            GUILayout.Space(10);

            if (buildReport != null)
            {
                DrawReport();
                GUILayout.Space(10);
                DrawUpload();
            }
        }

        public static BlockPublishOption GetOption()
        {
            return new BlockPublishOption { targetScene = blockScene, theme = blockTheme };
        }

        /// <summary>
        /// 빌드 설정
        /// - Scriptable Object 적용 칸
        /// - 빌드 버튼
        /// </summary>
        private static async UniTaskVoid DrawPublish()
        {
            GUILayout.Label("Publish", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            blockScene = EditorGUILayout.ObjectField("Scene", blockScene, typeof(SceneAsset), false) as SceneAsset;
            blockTheme = (BlockTheme)EditorGUILayout.EnumPopup("Theme", blockTheme);

            EditorGUILayout.EndVertical();
            if (GUILayout.Button("Publish"))
            {
                buildReport = WitchToolkitPipeline.PublishWithValidation(GetOption());

                // TODO: 플랫폼별 업로드
                if (buildReport.result == JBuildReport.Result.Success)
                {
                    // 썸네일 캡쳐
                    var thumbnailPath = Path.Combine(AssetBundleConfig.BundleExportPath, GetOption().ThumbnailKey);
                    CaptureTool.CaptureAndSave(thumbnailPath);
                    
                    
                    // 업로드 로딩창
                    EditorUtility.DisplayProgressBar("Witch Creator Toolkit", "uploading to server...", 1.0f);
                    await UniTask.Delay(5000);
                    EditorUtility.ClearProgressBar();
                    
                    // todo : 유니티 키 생성 api 결과에 따라 팝업창 메시지 다르게 변경할 것
                    // EditorUtility.DisplayDialog("Witch Creator Toolkit", successMsg, "OK");
                    EditorUtility.DisplayDialog("Witch Creator Toolkit", failedMsg, "OK");
                }
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
            GUILayout.Label("Report", EditorStyles.boldLabel);
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

        private static void DrawUpload()
        {
            GUILayout.Label("Upload", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            
            EditorGUILayout.LabelField("Result", uploadResult.ToString());

            EditorGUILayout.EndVertical();
        }

        private static async UniTask<bool> Upload(string bundleType)
        {
            var option = GetOption();

            var manifest = new JManifest
            {
                unityVersion = ToolkitConfig.UnityVersion,
                toolkitVersion = ToolkitConfig.WitchToolkitVersion,
                crc = AssetBundleTool.ReadManifest(bundleType, option.BundleKey),
            };
            uploadResult = UploadState.Uploading;
            
            var response = await WitchAPI.UploadBlock(option, manifest, bundleType);

            return response;
        }
    }
}