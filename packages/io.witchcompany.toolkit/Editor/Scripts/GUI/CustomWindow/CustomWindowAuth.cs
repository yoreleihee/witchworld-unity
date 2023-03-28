using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace WitchCompany.Toolkit.Editor
{
    public static class CustomWindowAuth
    {
        
        public static void ShowAuth()
        {
            EditorGUILayout.BeginVertical("box");
            
            EditorGUILayout.LabelField("Account");
            
            EditorGUILayout.EndVertical();
            
        }
    }
}