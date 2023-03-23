using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Module;

namespace WitchCompany.Toolkit.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(WitchBehaviour), true)]
    public sealed class WitchBehaviourEditor : UnityEditor.Editor
    {
        private static bool _isInitialized = false;

        private static readonly string[] ExcludedProperties = { "m_Script" };
        
        private static GUIStyle _areaStyle;
        private static GUIStyle _titleStyle;
        private static GUIStyle _descriptionStyle;
        
        private static void Initialize()
        {
            if (_isInitialized) return;
            _isInitialized = true;

            var offset = new RectOffset(8, 8, 8, 8);
            
            _areaStyle = new GUIStyle
            {
                border = offset,
                padding = offset,
                normal =
                {
                    background = Texture2D.grayTexture,
                    textColor = Color.white
                }
            };
            
            _titleStyle = new GUIStyle
            {
                fontStyle = FontStyle.Bold,
                fontSize = 24,
                wordWrap = true,
                //contentOffset = new Vector2(0, 3),
                normal =
                {
                    textColor = Color.white
                }
            };
            
            _descriptionStyle = new GUIStyle
            {
                fontSize = 12,
                wordWrap = true,
                richText = true,
                normal =
                {
                    textColor = new Color(1, 1, 1, .75f)
                }
            };
            
            Debug.Log("Style Rebuild");
        }

        public override void OnInspectorGUI()
        {
            Initialize();
            serializedObject.Update();
            
            var editorTarget = target as WitchBehaviour;
            if (editorTarget != null)
            {
                GUILayout.Space(4);
                GUILayout.BeginVertical(_areaStyle);
                {
                    GUILayout.Label(editorTarget.BehaviourName, _titleStyle);
                    GUILayout.Space(8);
                    GUILayout.Label(editorTarget.Description, _descriptionStyle);
                }
                GUILayout.EndVertical();
                GUILayout.Space(4);
            }
            
            //DrawDefaultInspector();
            DrawPropertiesExcluding(serializedObject, ExcludedProperties);

            serializedObject.ApplyModifiedProperties();
        }
    }
}