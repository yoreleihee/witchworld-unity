using System;
using UnityEngine;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [Flags]
    public enum PlatformType
    {
        None = 0,
        Standalone = 1 << 0,
        Webgl = 1 << 1,
        WebglMobile = 1 << 2,
        Android = 1 << 3,
        [InspectorName("IOS")] 
        Ios = 1 << 4,
        [InspectorName("VR")] 
        Vr = 1 << 5
    }
}