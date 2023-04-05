using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WitchCompany.Toolkit.Editor.Tool;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class ComponentWhiteList
    {
        private static string[] componentTypeWhiteList = new string[]
        {
            "Collider",
            "CapsuleCollider",
            "Transform",
            "MeshFilter",
            "WitchBehaviour",
            "Light"
        };
        
        public static void FindComponent()
        {
            var scene = SceneManager.GetActiveScene();
            
            var rootObject = scene.GetRootGameObjects()[0].transform;
            var children = HierarchyTool.GetAllChildren(rootObject);

            // 오브젝트 단위
            foreach (var tr in children)
            {
                var components = tr.GetComponents<Component>();

                // 컴포넌트 단위
                foreach (var c in components)
                {
                    var derived = c.GetType();
                    // whitelist에서 컴포넌트 존재 여부 탐색
                    while (derived != null)
                    {
                        var type = derived.FullName.Split(".")[^1];
                        var check = Array.Exists(componentTypeWhiteList, x => x == type);
                        
                        // white list에 있으면 통과
                        if (check)
                        {
                            // Debug.Log("통과");
                            break;
                        }

                        derived = derived.BaseType;
                    }
                    if(derived == null)
                        Debug.Log($"실패\n{c.GetType().FullName}", c);
                }
            }
        }
    }
}