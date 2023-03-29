using UnityEditor;
using UnityEngine;

namespace WitchCompany.Toolkit.Editor
{
    public class CustomWindowValidation
    {

        public static void ShowValidation()
        {
            EditorGUILayout.LabelField("Validation", EditorStyles.boldLabel);
            GUILayout.Label("현재 유효성 검사는 빌드 진행시 자동으로 진행됩니다.");
            GUILayout.Label("추후 3가지 유효성 검사를 제공할 예정입니다.");
            GUILayout.Label("  - 최적화 검사");
            GUILayout.Label("  - 씬 규칙 검사");
            GUILayout.Label("  - 업로드 규칙 검사");
        }
    }
}