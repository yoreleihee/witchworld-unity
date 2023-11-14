using System;
using System.IO;
using PlasticGui;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.Tool;
using WitchCompany.Toolkit.Editor.Validation;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Editor.GUI
{
    public class CustomWindowExportBundle
    {
        private static Vector2 scrollPos;
        private static ValidationReport validationReport;
        private static JBuildReport buildReport;
        public static void ShowExportBundle()
        {
            DrawExportBundle();
            
            GUILayout.Space(10);
            
            if (GUILayout.Button("Export"))
            {
                OnClickBuild();
            }

            if (validationReport != null)
            {
                DrawReport();
            }
        }

        private static void DrawExportBundle()
        {
            GUILayout.Label("Export Bundle", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");

            var check = new EditorGUI.ChangeCheckScope();
            using (new EditorGUILayout.HorizontalScope())
            {
                using (check)
                {
                    var prefab = EditorGUILayout.ObjectField("Prefab", ExportBundleConfig.Prefab, typeof(GameObject), false) as GameObject;
                    if (check.changed)
                    {
                        ExportBundleConfig.Prefab = prefab;
                        validationReport = null;
                    }
                }
            }
            
            using (new EditorGUILayout.HorizontalScope())
            {
                using (check)
                {
                    if (ExportBundleConfig.Prefab != null)
                    {
                        var bytes = AssetTool.GetFileSizeByte(ExportBundleConfig.PrefabPath);
                        var sizeKb = Math.Round((double)bytes / 1024, 3);
                        EditorGUILayout.LabelField("File Size", $"{sizeKb} / {ExportBundleConfig.MaxProductSizeKb} KB");
                    }
                }
            }
            
            EditorGUILayout.EndVertical();
        }


        private static void DrawReport()
        {
            GUILayout.Label("Report", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Result", validationReport.result.ToString());

            if (validationReport.result == ValidationReport.Result.Success)
            {
                var path = Path.Combine(ExportBundleConfig.BundleExportPath, ExportBundleConfig.Prefab.name);
                EditorGUILayout.LabelField("Path", path);
            }
            else
            {
                GUILayout.Space(5);
                GUILayout.Label("Message", EditorStyles.boldLabel);
                EditorGUILayout.BeginVertical("box");
        
                // 에러 메시지 출력
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            
                var preErrorTag = "";
                foreach (var error in validationReport.errors)
                {
                    if (error == null) return;
                    
                    // 이전 tag와 값이 다르면 tag 출력
                    if (preErrorTag != error.tag)
                    {
                        GUILayout.Space(10);
                        GUILayout.Label(error.tag, EditorStyles.boldLabel);   
                        
                        preErrorTag = error.tag;
                    }

                    // 로그 종류에 따라 버튼 style 변경
                    if (error.context == null)
                        GUILayout.Label(error.message, CustomWindow.LogTextStyle);
                    else
                    {
                        if (GUILayout.Button(error.message, CustomWindow.LogButtonStyle))
                            EditorGUIUtility.PingObject(error.context);
                    }
                }
                
                EditorGUILayout.EndScrollView();
                EditorGUILayout.EndVertical();

            }
            EditorGUILayout.EndVertical();
        }
            
        private static void OnClickBuild()
        {
            validationReport = null;
            
            CustomWindow.IsInputDisable = true;  
            EditorUtility.DisplayProgressBar("Witch Creator Toolkit", "Build...", 1.0f);

            validationReport = PrefabValidator.ValidationCheck();

            if (validationReport.result == ValidationReport.Result.Success)
            {
                buildReport = PrefabBuildPipeline.BuildReport(AssetBundleConfig.Standalone);
                buildReport = PrefabBuildPipeline.BuildReport(AssetBundleConfig.Webgl);
                buildReport = PrefabBuildPipeline.BuildReport(AssetBundleConfig.WebglMobile);
                buildReport = PrefabBuildPipeline.BuildReport(AssetBundleConfig.Android);
                buildReport = PrefabBuildPipeline.BuildReport(AssetBundleConfig.Ios);
            }
            
            EditorUtility.ClearProgressBar();
            CustomWindow.IsInputDisable = false;

            if(validationReport != null) return;
            
            var msg = buildReport.result == JBuildReport.Result.Success ? "빌드 성공" : "빌드 실패";
            EditorUtility.DisplayDialog("Witch Creator Toolkit", msg, "OK");
        }
    }
}