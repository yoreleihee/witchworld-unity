using System.Collections.Generic;
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
    }
}