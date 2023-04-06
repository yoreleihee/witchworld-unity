using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WitchCompany.Toolkit.Editor.Tool
{
    public static class StaticRevertTool
    {
        private static Dictionary<int, StaticEditorFlags> _staticObjects = new();

        /// <summary>
        /// static 오브젝트의 static 해제
        /// static flag 상태 저장
        /// </summary>
        public static void SaveAndClearFlags()
        {
            _staticObjects ??= new Dictionary<int, StaticEditorFlags>();
            _staticObjects.Clear();

            foreach (var tr in GetAllTransforms())
            {
                var go = tr.gameObject;
                if (!go.isStatic) continue;
                
                // 기존 static 설정 상태 저장
                var id = ObjectTool.GetLocalIdentifier(go);
                var flag = GameObjectUtility.GetStaticEditorFlags(go);

                // 저장
                _staticObjects.Add(id, flag);
                // static 해제
                GameObjectUtility.SetStaticEditorFlags(go, 0);
                //Debug.Log($"{go.name}({id}) : {flag.ToString()}");
            }
        }
        
        public static void RevertFlags()
        {
            if(_staticObjects is not {Count: > 0}) return;
            
            foreach (var tr in GetAllTransforms())
            {
                var go = tr.gameObject;
                var id = ObjectTool.GetLocalIdentifier(go);

                if (_staticObjects.TryGetValue(id, out var flag))
                {
                    GameObjectUtility.SetStaticEditorFlags(go, flag);
                    //Debug.Log($"{go.name}({id}) : {flag.ToString()}");
                }
            }
        }

        private static List<Transform> GetAllTransforms()
        {
            var scn = SceneManager.GetActiveScene();
            var parent = scn.GetRootGameObjects()[0];
            return HierarchyTool.GetAllChildren(parent.transform);
        }
    }
}