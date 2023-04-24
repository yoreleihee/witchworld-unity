using System;
using UnityEngine;

namespace WitchCompany.Toolkit.Attribute
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ShowIfAttribute : PropertyAttribute
    {
        public string EnumName { get; private set; }
        public Enum EnumValue { get; private set; }

        public ShowIfAttribute(string enumName, object enumValueObj)
        {
            if(enumValueObj is not Enum enumValue)
                throw new ArgumentNullException(nameof(enumValue), "This parameter must be an enum value.");

            EnumName = enumName;
            EnumValue = enumValue;
        }
    }
}