using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;

namespace WitchCompany.Toolkit.Editor.GUI
{
    public static class CustomWindowAdmin
    {
        private const int maxBlockNameLength = 20;
        
        private static string _thumbnailPath;
        private static string _pathName;
        private static JLanguageString _blockName = new JLanguageString();
        private static BlockType _blockType;
        private static int _selectedUnityKey = 0;

        private static Texture2D thumbnailImage;
        // todo : 유니티 키 리스트 api에서 받은 값 저장
        private static List<string> unityKeyList = new List<string>() { "ryu(made by jj)", "ryu(made by hh)", "ryu(made by kk)"};
        
        public static void ShowAdmin()
        {
            
            DrawUnityKey();

            GUILayout.Space(10);
            
            DrawBlockConfig();
            
        }

        private static async UniTaskVoid DrawUnityKey()
        {
            GUILayout.Label("Unity Key", EditorStyles.boldLabel);

            // 키 선택
            EditorGUILayout.BeginHorizontal("box");
            
            // todo : unity key 페이징 조회 api와 연동
            _selectedUnityKey = EditorGUILayout.Popup("key list", _selectedUnityKey, unityKeyList.ToArray());
            
            if (GUILayout.Button("refresh"))
            {
                CustomWindow.IsInputDisable = true;
                
                EditorUtility.DisplayProgressBar("Witch Creator Toolkit", "Getting unity key list from server....", 1.0f);
                await UniTask.Delay(3000);
                EditorUtility.ClearProgressBar();
                
                CustomWindow.IsInputDisable = false;
            }
            EditorGUILayout.EndHorizontal();
            
        }
        
        private static async UniTaskVoid DrawBlockConfig()
        {
            GUILayout.Label("Block Config", EditorStyles.boldLabel);
            
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("thumbnail", _thumbnailPath, EditorStyles.textField);
            if (GUILayout.Button("Select image..."))
            {
                _thumbnailPath = EditorUtility.OpenFilePanel("Witch Creator Toolkit", "", "jpg");
            }
            EditorGUILayout.EndHorizontal();

            // 정규식에 맞지 않을 경우 이전 값으로 되돌림
            var prePathName = _pathName;
            _pathName = EditorGUILayout.TextField("path name", _pathName);
            if (!Regex.IsMatch(string.IsNullOrEmpty(_pathName) ? "" : _pathName, AssetBundleConfig.ValidNameRegex))
                _pathName = prePathName;

            // 최대 글자수 넘으면 이전값으로 되돌림
            var preBlockNameKr = _blockName.kr;
            _blockName.kr = EditorGUILayout.TextField("block name (한글)",_blockName.kr);
            if (_blockName.kr?.Length > maxBlockNameLength)
                _blockName.kr = preBlockNameKr;

            var preBlockNameEn = _blockName.en;
            _blockName.en = EditorGUILayout.TextField("block name (영문)",_blockName.en);
            if (_blockName.en?.Length > maxBlockNameLength)
                _blockName.en = preBlockNameEn;

            _blockType = (BlockType)EditorGUILayout.EnumPopup("type", _blockType);
            
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("Publish"))
            {
                CustomWindow.IsInputDisable = true;
                
                // todo : 블록 생성 api 연동
                EditorUtility.DisplayProgressBar("Witch Creator Toolkit", "Uploading from server....", 1.0f);
                await UniTask.Delay(5000);
                EditorUtility.ClearProgressBar();
                
                // todo : 유니티 키 생성 api 결과에 따라 팝업창 메시지 다르게 변경할 것
                // EditorUtility.DisplayDialog("Witch Creator Toolkit", successMsg, "OK");
                EditorUtility.DisplayDialog("Witch Creator Toolkit", "블록 생성 성공", "OK");
                
                CustomWindow.IsInputDisable = false;
            }
        }
    }
}