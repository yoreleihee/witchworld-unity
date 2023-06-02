using System;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [Flags]
    public enum PlatformType
    {
        None = 0,
        Standalone = 1 << 0,
        Webgl = 1 << 1,
        Android = 1 << 2,
        Ios = 1 << 3
    }
}