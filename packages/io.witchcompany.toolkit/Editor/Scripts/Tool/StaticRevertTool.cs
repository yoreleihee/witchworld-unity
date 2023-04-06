using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using WitchCompany.Toolkit.Editor.DataStructure;

namespace WitchCompany.Toolkit.Editor.Tool
{
    public static class StaticRevertTool
    {
        private static List<Tuple<GameObject, StaticEditorFlags>> staticObjects;
        // private static List<> staticObjects;

        /// <summary>
        /// static 오브젝트의 static 해제
        /// static flag 상태 저장
        /// </summary>
        public static void Release(BlockPublishOption option)
        {
            try
            {
                Debug.Log("Static Release!!");
                
                staticObjects ??= new List<Tuple<GameObject, StaticEditorFlags>>();
            
                var transforms = GameObject.FindObjectsOfType<Transform>(true);

                foreach (var tr in transforms)
                {
                    var go = tr.gameObject;
                    if (go.isStatic)
                    {
                        // 기존 static 설정 상태 저장
                        var flag = GameObjectUtility.GetStaticEditorFlags(go);
                        var tuple = new Tuple<GameObject, StaticEditorFlags>(go, flag);
                        staticObjects.Add(tuple);
                        // static 해제
                        GameObjectUtility.SetStaticEditorFlags(go, 0);
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        
        public static void Revert()
        {
            foreach (var obj in staticObjects)
            {
                // 게임오브젝트에 static 설정을 원래대로
                GameObjectUtility.SetStaticEditorFlags(obj.Item1, obj.Item2);
            }
            staticObjects.Clear();
            Debug.Log("Static Revert!!");
        }
    }
}