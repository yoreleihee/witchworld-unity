using System;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.Tool;

namespace WitchCompany.Toolkit.Editor.GUI
{
    public static class CustomWindowPublish
    {
        private static SceneAsset targetScene;
        private static BlockPublishOption blockPublishOption;
        public static JBuildReport buildReport;

        public static void ShowPublish()
        {
            // 빌드 정보
            DrawConfig();
            
            GUILayout.Space(10);
            
            if(buildReport != null)
                DrawReport();
        }

        /// <summary>
        /// 빌드 설정
        /// - Scriptable Object 적용 칸
        /// - 빌드 버튼
        /// </summary>
        private static void DrawConfig()
        {
            GUILayout.Label("Publish", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");

            // blockPublishOption = EditorGUILayout.ObjectField("blockPublishOption", blockPublishOption, typeof(BlockPublishOption), false) as BlockPublishOption;
            targetScene = EditorGUILayout.ObjectField("Scene", targetScene, typeof(SceneAsset), false) as SceneAsset;
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("Publish"))
            {
                buildReport = WitchToolkitPipeline.PublishWithValidation(targetScene);
                // buildReport = WitchToolkitPipeline.PublishWithValidation(blockPublishOption);
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
                // EditorGUILayout.LabelField("StartTime, ${buildReport.}");
                // 종료시간
                EditorGUILayout.LabelField("EndTime", buildReport.BuildEndedAt.ToString());
                // 소요시간 

                TimeSpan time = buildReport.BuildEndedAt - buildReport.BuildEndedAt;
                EditorGUILayout.LabelField("Duration", time.ToString());;

            }
            
            EditorGUILayout.EndVertical();
        }
    }
}