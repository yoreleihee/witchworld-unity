using NaughtyAttributes.Editor;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Attribute;

namespace WitchCompany.Toolkit.Editor.GUI.Attribute
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!Show(property)) return 0f;

            //return base.GetPropertyHeight(property, label);
            return EditorGUI.GetPropertyHeight(property, includeChildren: true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!Show(property)) return;
            
            EditorGUI.PropertyField(position, property, label);
        }

        private bool Show(SerializedProperty property)
        {
            var showIfAttribute = (ShowIfAttribute) attribute;
            if (showIfAttribute == null) return true;
            if (showIfAttribute.EnumValue == null) return true;

            
            //Debug.Log(showIfAttribute.EnumName + " " + showIfAttribute.EnumValue);
            var target = property.serializedObject.targetObject;
            if (target == null) return true;
            
            //Debug.Log(target);
                
            var value = PropertyUtility.GetEnumValue(target, showIfAttribute.EnumName);
            if (value == null) return true;
            
            //Debug.Log(value);
                
            return showIfAttribute.EnumValue.Equals(value);
        }
    }
}