using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace WitchCompany.Toolkit.Editor.Tool
{
    public static class ObjectTool
    {
        /// <summary>해당 오브젝트의 LocalIdentfierInFile 값을 가져온다.</summary>
        public static int GetLocalIdentifier(Object obj) {

            var inspectorModeInfo = typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);
            var serializedObject = new SerializedObject(obj);

            inspectorModeInfo.SetValue(serializedObject, InspectorMode.Debug, null);

            var localIdProp = serializedObject.FindProperty("m_LocalIdentfierInFile");

            return localIdProp.intValue;
        }
        
        /// <summary>해당 Scene에서 Missing Prefabs를 찾아준다.</summary>
        private static void FindMissingPrefabs()
        {
            var prefabList = new List<GameObject>();
            var instanceRootPrefabList = new List<GameObject>();;
            var anyPartOfPrefabList = new List<GameObject>();
            var missingPrefabInSceneList = new List<GameObject>();;
            var notMissingPrefabInSceneList  = new List<GameObject>();
            var missingPrefabInIsolatedScene = new List<string>();
            var allObjList = new List<GameObject>();
            var allObjArray = GameObject.FindObjectsOfType<Transform>();
            
            foreach (var a in allObjArray)
            {
                allObjList.Add(a.gameObject);
            }

            foreach (var a in allObjList)
            {
                // Prefab InstatnceRoot 찾기 
                if (PrefabUtility.IsAnyPrefabInstanceRoot(a.gameObject))
                {
                    instanceRootPrefabList.Add(a.gameObject);
                }
                // Prefab Any 찾기 
                if (PrefabUtility.IsPartOfAnyPrefab(a.gameObject))
                {
                    anyPartOfPrefabList.Add(a.gameObject);
                }
            }

            // 현재 Scene에서 Inspector : missing 여부 구분  
            foreach (var a in instanceRootPrefabList)
            {
                if (!PrefabUtility.IsPrefabAssetMissing(a.gameObject))
                {
                    notMissingPrefabInSceneList.Add(a.gameObject); // → (1)
                }
                else
                {
                    missingPrefabInSceneList.Add(a.gameObject);  // → (2)
                }
            }

            // (1) Isolated Scene 에서 missing 찾기
            foreach (var nm in notMissingPrefabInSceneList)
            {
                // isolated scene 열기 -> 모든 자식 가져오기
                var origin = PrefabUtility.GetCorrespondingObjectFromSource(nm);  
                var originPath = AssetDatabase.GetAssetPath(origin);
                // isolated scene 열기
                var contents = PrefabUtility.LoadPrefabContents(originPath);
                Transform[] allChildren = contents.GetComponentsInChildren<Transform>();
                
                // 자신 제외한 자식 검사
                foreach (var objTrans in allChildren)
                {
                    var c = objTrans.gameObject;

                    if (c.name != contents.name)
                    {
                        if (PrefabUtility.IsPrefabAssetMissing(c))
                        {
                            missingPrefabInIsolatedScene.Add(c.name);
                        }
                    }
                }
                // isolated scene 닫기
                PrefabUtility.UnloadPrefabContents(contents);
                
                // 에러 지우기
                var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
                var type = assembly.GetType("UnityEditor.LogEntries");
                var clearMethodInfo = type.GetMethod("Clear");
                clearMethodInfo.Invoke(new object(), null);
                
                foreach (var m in missingPrefabInIsolatedScene)
                {
                    Debug.Log($"Missing Prefab이 발견되었습니다.({m})", GameObject.Find(m));
                }
            }

            // (2) Current Scene에서 missing 찾기
            foreach (var m in missingPrefabInSceneList)
            {
                Debug.Log($"Missing Prefab이 발견되었습니다.({m.name})", m);
            }
            var missingTotalCount = missingPrefabInIsolatedScene.Count + missingPrefabInSceneList.Count;
            Debug.LogError($"총 {missingTotalCount}개의 Missing Prefab이 발견되었습니다.");
        }
    }
}