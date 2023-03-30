using System;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Tool;
using WitchCompany.Toolkit.Module;

namespace WitchCompany.Toolkit.Editor.GUI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(WitchBehaviour), true)]
    public sealed class WitchBehaviourEditor : UnityEditor.Editor
    {
        private bool _isInstanceInitialized;
        private static long _timeStamp;
        
        private static bool _isInitialized;
        private static readonly string[] ExcludedProperties = { "m_Script" };
        private static WitchBlockManager _currentBlockManager;
        
        private static GUIStyle _areaStyle;
        private static GUIStyle _titleStyle;
        private static GUIStyle _descriptionStyle;
        private static GUIStyle _counterStyle;
        
        private static void Initialize()
        {
            if (_isInitialized) return;
            _isInitialized = true;

            // 오프셋
            var offset = new RectOffset(8, 8, 8, 8);
            
            // 영역 스타일
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
            
            // 제목 스타일
            _titleStyle = new GUIStyle
            {
                fontStyle = FontStyle.Bold,
                fontSize = 24,
                wordWrap = true,
                normal =
                {
                    textColor = Color.white
                }
            };
            
            // 설명 스타일
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
            
            // 카운터 스타일
            _counterStyle = new GUIStyle
            {
                fontSize = 12,
                wordWrap = true,
                richText = true,
                normal =
                {
                    textColor = new Color(1, 1, 0.2f, .75f)
                }
            };
        }

        private void InitializeInstance()
        {
            if(_isInstanceInitialized) return;
            _isInstanceInitialized = true;

            var timeStamp = CommonTool.GetCurrentTimeStamp();
            if(_timeStamp == timeStamp) return;
            _timeStamp = timeStamp;

            if (_currentBlockManager == null) _currentBlockManager = FindObjectOfType<WitchBlockManager>();
            _currentBlockManager.FindWitchBehaviours();
        }

        public override void OnInspectorGUI()
        {
            Initialize();
            InitializeInstance();
            serializedObject.Update();
            
            var editorTarget = target as WitchBehaviour;
            if (editorTarget != null)
            {
                GUILayout.Space(4);
                GUILayout.BeginVertical(_areaStyle);
                {
                    // 이름 렌더링
                    GUILayout.Label(editorTarget.BehaviourName, _titleStyle);
                    GUILayout.Space(8);
                    // 설명 렌더링
                    GUILayout.Label(editorTarget.Description, _descriptionStyle);
                    GUILayout.Space(12);
                    // 개수 렌더링
                    if (_currentBlockManager != null && _currentBlockManager.BehaviourCounter.ContainsKey(editorTarget.GetType()))
                    {
                        var count = editorTarget.MaximumCount <= 0 ? "제한없음" : 
                            $"{_currentBlockManager.BehaviourCounter[editorTarget.GetType()].count}/{editorTarget.MaximumCount} 개";
                        GUILayout.Label($"배치 개수 제한: {count}", _counterStyle);
                    }
                    else
                    {
                        GUILayout.Label($"배치 개수 제한: 최대 {editorTarget.MaximumCount}개", _counterStyle);
                    }
                    
                }
                GUILayout.EndVertical();
                GUILayout.Space(4);
            }
            
            DrawPropertiesExcluding(serializedObject, ExcludedProperties);

            serializedObject.ApplyModifiedProperties();
        }
    }
}