using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs.Enum;

// ReSharper disable InconsistentNaming

namespace WitchCompany.Toolkit.Editor.Configs
{
    /// <summary>
    /// 현재 툴킷의 설정을 저장한다.
    /// </summary>
    public static class ToolkitConfig
    {
        private const string Prefs_CurrentControlPanelType = "current_control_panel_type";
        private const string Prefs_LogOption = "log_option";
        private const string Prefs_ValidateLevel = "validate_level";

        /// <summary>현재 컨트롤 패널 타입</summary>
        public static ControlPanelType CurrControlPanelType
        {
            get => (ControlPanelType)EditorPrefs.GetInt(Prefs_CurrentControlPanelType, (int)ControlPanelType.Disabled);
            set => EditorPrefs.SetInt(Prefs_CurrentControlPanelType, (int) value);
        }
        
        /// <summary>현재 로그 레벨</summary>
        public static LogLevel CurrLogLevel
        {
            get => (LogLevel)EditorPrefs.GetInt(Prefs_LogOption, (int)LogLevel.None);
            set => EditorPrefs.SetInt(Prefs_LogOption, (int) value);
        }
        
        /// <summary>현재 유효성 검사 수준</summary>
        public static ValidateLevel CurrValidateLevel
        {
            get => (ValidateLevel)EditorPrefs.GetInt(Prefs_ValidateLevel, (int)ValidateLevel.Essentials);
            set => EditorPrefs.SetInt(Prefs_ValidateLevel, (int) value);
        }

        /// <summary>현재 유니티 버전</summary>
        public static string UnityVersion => Application.unityVersion;
    }
}