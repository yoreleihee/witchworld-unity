using System.Reflection;
using UnityEditor;
using UnityEngine;

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
    }
}