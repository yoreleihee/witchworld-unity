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
        private static string _name;
        private static string _version;
        private static string _description;
        private static string _capacity;
        private static string _releaseStatus;
        private static List<string> _tags;

        public static string Name {
            get => _name;
            set => _name = value;
        }
        public static string Version { get; set; }
        public static string Desciption { get; set; }
        public static string Capacity { get; set; }
        public static string ReleaseStatus { get; set; }
        public static List<string> Tags { get; set; }


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
                Debug.Log("Publish Click!");
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
            GUILayout.Label("Result", EditorStyles.boldLabel);
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

        

        // Build 정보
        public static void DrawContentInfo(string name, string version, string description, string capacity,
            string releaseStatus, List<string> tags)
        {
            EditorGUILayout.LabelField("Name: " + name);
            EditorGUILayout.LabelField("Version: " + version.ToString());
            EditorGUILayout.LabelField("Platform: " + EditorUserBuildSettings.activeBuildTarget);
            EditorGUILayout.LabelField("Description: " + description);
            if (capacity != null)
                EditorGUILayout.LabelField("Capacity: " + capacity);
            EditorGUILayout.LabelField("Release: " + releaseStatus);
            if (tags != null)
            {
                string tagString = "";
                for (int i = 0; i < tags.Count; i++)
                {
                    if (i != 0) tagString += ", ";
                    tagString += tags[i];
                }
                EditorGUILayout.LabelField("Tags: " + tagString);
            }
        }
        
        public static void DrawContentInfo()
        {
            EditorGUILayout.LabelField("Name: " + PlayerSettings.productName);
            EditorGUILayout.LabelField("Version: " + Application.version);
            EditorGUILayout.LabelField("Platform: " + EditorUserBuildSettings.activeBuildTarget);
            
            // var platformPath = Path.Combine(BasePath, "Builds", EditorUserBuildSettings.activeBuildTarget.ToString());
            // var buildPath = Path.Combine(platformPath, version);
        }
    }
}