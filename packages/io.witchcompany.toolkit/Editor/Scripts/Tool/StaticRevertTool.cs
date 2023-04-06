using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Editor.Tool
{
    public static class StaticRevertTool
    {
        private static Dictionary<int, StaticEditorFlags> _staticObjects = new();

        /// <summary>
        /// static 오브젝트의 static 해제
        /// static flag 상태 저장
        /// </summary>
        public static ValidationReport SaveAndClearFlags()
        {
            var report = new ValidationReport();
                
            // staticObjects Null 체크 후 새로 생성, 혹은 비우기
            _staticObjects ??= new Dictionary<int, StaticEditorFlags>();
            _staticObjects.Clear();

            // 모은 오브젝트를 순회하며,
            var transforms = GetAllTransforms();
            for (var i = 0; i < transforms.Count; i++)
            {
                // 게임오브젝트 가져오기
                var go = transforms[i].gameObject;
                // Static 이 아니라면 다음으로..
                if (!go.isStatic) continue;

                // 해당 오브젝트의 id 가져오기
                // 해당 오브젝트의 static flag 가져오기
                var flag = GameObjectUtility.GetStaticEditorFlags(go);

                // 캐싱 시도 후 플래그 해제
                if (_staticObjects.TryAdd(i, flag))
                {
                    //Debug.Log($"static 캐싱: {go.name}({i}): {flag}", go);
                    GameObjectUtility.SetStaticEditorFlags(go, 0);
                }
                // 실패시,
                else
                    report.Append($"static 캐싱에 실패했습니다: {go.name}", ValidationTag.TagBadObject, go);
            }

            return report;
        }
        
        public static void RevertFlags()
        {
            // staticObjects가 없으면 종료
            if(_staticObjects is not {Count: > 0}) return;
            
            // 모은 오브젝트를 순회하며,
            var transforms = GetAllTransforms();
            for (var i = 0; i < transforms.Count; i++)
            {
                // 게임오브젝트 가져오기
                var go =  transforms[i].gameObject;

                // 해당 오브젝트의 id가 staticObjects에 캐싱되어 있다면,
                if (_staticObjects.TryGetValue(i, out var flag))
                {
                    // static flag 되돌리기
                    //Debug.Log($"static 복구: {go.name}({i}): {flag}", go);
                    GameObjectUtility.SetStaticEditorFlags(go, flag);
                }
            }
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