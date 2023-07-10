using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.Tool;
using WitchCompany.Toolkit.Editor.Validation;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Editor.GUI
{
    public class CustomWindowProduct
    {
        private static JBuildReport buildReport;
        public static void ShowProduct()
        {
            DrawProduct();
            
            GUILayout.Space(10);
            
            if (GUILayout.Button("Publish"))
            {
                OnClickPublish();
            }

            if (buildReport != null)
            {
                DrawReport();
            }
        }

        private static void DrawProduct()
        {
            GUILayout.Label("Product", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");

            var check = new EditorGUI.ChangeCheckScope();
            using (new EditorGUILayout.HorizontalScope())
            {
                using (check)
                {
                    var product = EditorGUILayout.ObjectField("Prefab", ProductConfig.Prefab, typeof(GameObject), false) as GameObject;
                    if (check.changed)
                        ProductConfig.Prefab = product;

                }
            }
            
            using (new EditorGUILayout.HorizontalScope())
            {
                using (check)
                {
                    if (ProductConfig.Prefab != null)
                    {
                        EditorGUILayout.LabelField("File", $"{AssetTool.GetFileSizeByte(ProductConfig.PrefabPath)} / {ProductConfig.MaxProductKB} KB");
                    }
                }
            }
            
            EditorGUILayout.EndVertical();
        }


        private static void DrawReport()
        {
            GUILayout.Label("Report", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            using (new EditorGUILayout.VerticalScope("box"))
            {
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    EditorGUILayout.LabelField("Result", buildReport.result.ToString());
                    
                    if (buildReport.result == JBuildReport.Result.Success)
                    {
                        var path = Path.Combine(ProductConfig.BundleExportPath, ProductConfig.Prefab.name); 
                        EditorGUILayout.LabelField("Path", path);
                    }
                }
            }
            EditorGUILayout.EndVertical();
        }
            
        private static void OnClickPublish()
        {
            CustomWindow.IsInputDisable = true;
            EditorUtility.DisplayProgressBar("Witch Creator Toolkit", "Publish...", 1.0f);
            // 최대 용량 검사
            var validationReport = ProductValidator.ValidationCheck();

            if (validationReport.result == ValidationReport.Result.Success)
            {
                buildReport = ProductBuildPipeline.PublishWithBuildReport();
            }
            else
            {
                var msg = new StringBuilder();
                foreach (var error in validationReport.errors)
                {
                    msg.Append(error.message + "\n");
                }
                
                EditorUtility.DisplayDialog("Witch Creator Toolkit", msg.ToString(), "OK");
            }
            
            EditorUtility.ClearProgressBar();
            CustomWindow.IsInputDisable = false;
        }
    }
}