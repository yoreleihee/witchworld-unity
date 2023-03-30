using System;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    /// <summary>어디까지 로그를 표시할지 여부</summary>
    [Flags]
    public enum LogLevel
    {
        None = 0,
        API = 1 << 0,
        Validation = 1 << 1,
        Tool = 1 << 2,
        Pipeline = 1 << 3
    }
}