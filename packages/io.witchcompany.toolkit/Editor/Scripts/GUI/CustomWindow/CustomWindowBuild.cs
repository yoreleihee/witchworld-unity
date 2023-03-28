using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace WitchCompany.Toolkit.Editor
{
    public static class CustomWindowBuild
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