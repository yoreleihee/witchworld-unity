using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace WitchCompany.Toolkit.Editor.Tool
{
    public static class HierarchyTool
    {
        public static List<Transform> GetAllChildren(Transform root)
        {
            var objects = new List<Transform>();
            GetChildrenRecursively(root, objects);
            return objects;
        }

        private static void GetChildrenRecursively(Transform parent, List<Transform> objects)
        {
            foreach (Transform child in parent)
            {
                objects.Add(child);
                GetChildrenRecursively(child, objects);
            }
        }
        
        /// <summary> 한글이 포함된 오브젝트 검출 </summary>
        public static void CheckKoreanName()
        {
            var allTransforms = GameObject.FindObjectsOfType<Transform>(true);
            // 한글이름 검출
            foreach (var tr in allTransforms)
            {
                if (Regex.IsMatch(tr.name, @"[ㄱ-ㅎ가-힣]"))
                {
                    Debug.LogWarning($"한글이름이 있습니다. 영문으로 변경해주세요. ({tr.name})", tr);
                }
            }
        }
        
        /// <summary> 해당 오브젝트에 적용된 머티리얼에서 쉐이더 찾기 </summary>
        public static void CheckShader(GameObject obj, Shader shader)
        {
            var materials = obj.GetComponent<Renderer>().sharedMaterials;
            Debug.Log(materials == null? "True" : "false");

            foreach (var mat in materials)
            {
                if (mat.shader == shader)
                {
                    Debug.Log($"{shader.name} 검출 : {mat.name}");
                } 
            }
        }
    }
}