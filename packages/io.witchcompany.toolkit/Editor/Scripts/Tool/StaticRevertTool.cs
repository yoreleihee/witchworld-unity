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
            // staticObjects Null 체크 후 새로 생성, 혹은 비우기
            _staticObjects ??= new Dictionary<int, StaticEditorFlags>();
            _staticObjects.Clear();
            Debug.Log("1. static 초기화");

            // 모은 오브젝트를 순회하며,
            foreach (var tr in GetAllTransforms())
            {
                // 게임오브젝트 가져오기
                var go = tr.gameObject;
                // Static 이 아니라면 다음으로..
                if (!go.isStatic) continue;
                
                // 해당 오브젝트의 id 가져오기
                var id = ObjectTool.GetLocalIdentifier(go);
                // 해당 오브젝트의 static flag 가져오기
                var flag = GameObjectUtility.GetStaticEditorFlags(go);
                Debug.Log($"2. 오브젝트 추가: {go.name}({id}): {flag}");

                // 캐싱
                _staticObjects.Add(id, flag);
                // static flag 해제
                GameObjectUtility.SetStaticEditorFlags(go, 0);
            }
            
            Debug.Log($"3. static 해제 성공");
        }
        
        public static void RevertFlags()
        {
            // staticObjects가 없으면 종료
            if(_staticObjects is not {Count: > 0}) return;
            
            Debug.Log($"4. static 복구 시작");
            // 모은 오브젝트를 순회하며,
            foreach (var tr in GetAllTransforms())
            {
                // 게임오브젝트 가져오기
                var go = tr.gameObject;
                // 해당 오브젝트의 id 가져오기
                var id = ObjectTool.GetLocalIdentifier(go);

                // 해당 오브젝트의 id가 staticObjects에 캐싱되어 있다면,
                if (_staticObjects.TryGetValue(id, out var flag))
                {
                    // static flag 되돌리기
                    GameObjectUtility.SetStaticEditorFlags(go, flag);
                    Debug.Log($"5. 오브젝트 돌리기: {go.name}({id}): {flag}");
                }
            }
            
            Debug.Log($"6. static 복구 종료");
        }

        // 현재 씬의 모든 오브젝트 가져오기
        private static List<Transform> GetAllTransforms()
        {
            var scn = SceneManager.GetActiveScene();
            var parent = scn.GetRootGameObjects()[0];
            return HierarchyTool.GetAllChildren(parent.transform);
        }
    }
}