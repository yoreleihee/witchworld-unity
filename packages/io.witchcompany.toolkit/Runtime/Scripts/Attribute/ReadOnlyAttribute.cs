using System;
using UnityEngine;

namespace WitchCompany.Toolkit.Attribute
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyAttribute : PropertyAttribute
    {
    }
}