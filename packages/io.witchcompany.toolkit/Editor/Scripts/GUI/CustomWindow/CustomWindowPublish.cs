using System;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.Tool;

namespace WitchCompany.Toolkit.Editor.GUI
{
    public static class CustomWindowPublish
    {
        private static SceneAsset blockScene;
        private static BlockTheme blockTheme;
        public static JBuildReport buildReport;

        public static void ShowPublish()
        {
            // 빌드 정보
            DrawConfig();
            
            GUILayout.Space(10);
            
            if(buildReport != null)
                DrawReport();
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
        private static void DrawConfig()
        {
            GUILayout.Label("Publish", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            blockScene = EditorGUILayout.ObjectField("Scene", blockScene, typeof(SceneAsset), false) as SceneAsset;
            blockTheme = (BlockTheme)EditorGUILayout.EnumPopup("Theme", blockTheme);
            
            EditorGUILayout.EndVertical();
            if (GUILayout.Button("Publish"))
            {
                buildReport = WitchToolkitPipeline.PublishWithValidation(GetOption());
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
    }
}