using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
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
        private static GUIStyle _docsButtonStyle;
        private static GUIContent _docsIconContent;
        
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
            _docsButtonStyle = new GUIStyle
            {
                fontSize = 12,
                wordWrap = true,
                richText = true,
                alignment = TextAnchor.MiddleCenter,
                fixedWidth = 50,
                normal =
                {
                    background = Texture2D.linearGrayTexture,
                    textColor = Color.white
                },
            };

            _docsIconContent = new GUIContent
            {
                
            };
        }

        private void InitializeInstance()
        {
            if(Application.isPlaying) return;
            
            if(_isInstanceInitialized) return;
            _isInstanceInitialized = true;

            var timeStamp = CommonTool.GetCurrentTimeStamp();
            if(_timeStamp == timeStamp) return;
            _timeStamp = timeStamp;

            _currentBlockManager ??= FindObjectOfType<WitchBlockManager>();
            if(_currentBlockManager != null)
                _currentBlockManager.FindWitchBehaviours(ToolkitConfig.UnityVersion, ToolkitConfig.WitchToolkitVersion);
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
                    GUILayout.BeginHorizontal();
                    // 이름 렌더링
                    GUILayout.Label(editorTarget.BehaviourName, _titleStyle);
                    // 문서 버튼 렌더링
                    if (GUILayout.Button("docs", _docsButtonStyle))
                    {
                        Application.OpenURL(editorTarget.DocumentURL);
                    }
                    GUILayout.EndHorizontal();
                    
                    
                    GUILayout.Space(8);
                    // 설명 렌더링
                    GUILayout.Label(editorTarget.Description, _descriptionStyle);
                    GUILayout.Space(12);
                    // 개수 렌더링
                    if (!Application.isPlaying && _currentBlockManager != null && _currentBlockManager.BehaviourCounter.ContainsKey(editorTarget.GetType()))
                    {
                        var count = editorTarget.MaximumCount <= 0 ? "제한없음" : 
                            $"{_currentBlockManager.BehaviourCounter[editorTarget.GetType()].count}/{editorTarget.MaximumCount} 개";
                        GUILayout.Label($"배치 개수 제한: {count}", _counterStyle);
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