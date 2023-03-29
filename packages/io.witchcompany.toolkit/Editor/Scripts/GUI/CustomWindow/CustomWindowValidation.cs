using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Validation;

namespace WitchCompany.Toolkit.Editor
{
    public class CustomWindowValidation
    {
        private static ValidationReport validationReport;
        private static List<string> errMsgs = new();
        public static void ShowValidation()
        {
            GUILayout.Label("현재 유효성 검사는 빌드 진행시 자동으로 진행됩니다.");
            GUILayout.Label("추후 3가지 유효성 검사를 제공할 예정입니다.");
            GUILayout.Label("  - 최적화 검사");
            GUILayout.Label("  - 씬 규칙 검사");
            GUILayout.Label("  - 업로드 규칙 검사");
            
            
            GUILayout.Space(30);
            DrawSceneVital();
            GUILayout.Space(10);

            if (validationReport != null)
                DrawReport();
        }

        private static void DrawSceneVital()
        {

            GUILayout.Label("Scene Vital", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Vertex", OptimizationValidator.GetMeshVertex().ToString());
            EditorGUILayout.LabelField("Unique Material", OptimizationValidator.GetUniqueMaterialCount().ToString());
            EditorGUILayout.LabelField("Texture", OptimizationValidator.GetTextureMB()+ " MB");
            EditorGUILayout.LabelField("Light Map", OptimizationValidator.GetLightMapMB()+ " MB");

            EditorGUILayout.EndVertical();
            
            
            if (GUILayout.Button("Validation Check"))
            {
                validationReport = OptimizationValidator.ValidationCheck();
                errMsgs = validationReport.errMessages;
            }
        }
        
        
        private static void DrawReport()
        {
            GUILayout.Label("Report", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Result", validationReport.result.ToString());
            EditorGUILayout.EndVertical();
            
            if (validationReport.result == ValidationReport.Result.Failed)
            {
                GUILayout.Space(5);
                GUILayout.Label("Message", EditorStyles.boldLabel);
                EditorGUILayout.BeginVertical("box");
            
            
                foreach (var err in validationReport.errMessages)
                {
                    // GUILayout.Label(validationReport.errMessages.Count.ToString());
                    GUILayout.Label(err);
                    GUILayout.Space(5);
                }
                EditorGUILayout.EndVertical();
            }
            
        }
    }
}