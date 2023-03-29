using System.Collections.Generic;
using System.IO;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.Tool;

namespace WitchCompany.Toolkit.Editor
{
    public static class CustomWindowPublish
    {
        private static BlockPublishOption blockPublishOption;
        public static JBuildReport buildReport;
        
        public static void ShowPublish()
        {
            // 빌드 정보
            DrawSetting();
            
            GUILayout.Space(10);
            
            if(buildReport != null)
                DrawReport();
        }

        /// <summary>
        /// 빌드 설정
        /// - Scriptable Object 적용 칸
        /// - 빌드 버튼
        /// </summary>
        private static void DrawSetting()
        {
            GUILayout.Label("Publish", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");

            blockPublishOption = EditorGUILayout.ObjectField("Block Publish Option", blockPublishOption, typeof(BlockPublishOption), false) as BlockPublishOption;

            if (GUILayout.Button("Publish"))
            {
                buildReport = WitchToolkitPipeline.PublishWithValidation(blockPublishOption);
            }
            EditorGUILayout.EndVertical();
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
                EditorGUILayout.LabelField("totalSize", CommonTool.ByteToMb(buildReport.totalSizeByte, 2)+" MB");
                EditorGUILayout.LabelField("BuildEndedAt", buildReport.BuildEndedAt.ToString());
            }
            
            EditorGUILayout.EndVertical();
        }
    }
}