using System;
using UnityEditor;
using UnityEngine;
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
        public static JBuildReport buildReport;
        private static UploadState uploadResult = UploadState.None;
        
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
        private static void DrawPublish()
        {
            GUILayout.Label("Publish", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            blockScene = EditorGUILayout.ObjectField("Scene", blockScene, typeof(SceneAsset), false) as SceneAsset;
            blockTheme = (BlockTheme)EditorGUILayout.EnumPopup("Theme", blockTheme);
            
            EditorGUILayout.EndVertical();
            if (GUILayout.Button("Publish"))
            {
                buildReport = WitchToolkitPipeline.PublishWithValidation(GetOption());

                if (buildReport.result == JBuildReport.Result.Success) 
                    Upload();
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
                EditorGUILayout.LabelField("ExportPath", buildReport.exportPath);
                EditorGUILayout.LabelField("TotalSize", $"{CommonTool.ByteToMb(buildReport.totalSizeByte, 2)} MB");
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
        public static async void Upload()
        {
            var option = GetOption();

            var manifest = new JManifest
            {
                unityVersion = ToolkitConfig.UnityVersion,
                toolkitVersion = ToolkitConfig.WitchToolkitVersion,
                crc = "0",
            };
            uploadResult = UploadState.Uploading;
            
            var response = await WitchAPI.UploadBlock(option, manifest);

            uploadResult = response ? UploadState.Success : UploadState.Failed;
        }
    }
}