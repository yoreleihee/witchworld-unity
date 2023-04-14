using Newtonsoft.Json;
using UnityEditor;
using WitchCompany.Toolkit.Editor.DataStructure;

namespace WitchCompany.Toolkit.Editor.Configs
{
    /// <summary>
    /// 툴킷을 통해 로그인한 정보
    /// </summary>
    public static class AuthConfig
    {
        private const string Prefs_Email = "auth_email";
        private const string Prefs_Password = "auth_password";
        private const string Prefs_NickName = "auth_nick_name";
        private const string Prefs_LoginTime = "auth_login_time";
        private const string Prefs_Auth = "auth_auth";
        private const string Prefs_Admin = "auth_admin";
        private const string Prefs_LoginState = "auth_login_state";

        public static string Email
        {
            get => EditorPrefs.GetString(Prefs_Email, "");
            set => EditorPrefs.SetString(Prefs_Email, value);
        }
        
        public static string Password
        {
            get => EditorPrefs.GetString(Prefs_Password, "");
            set => EditorPrefs.SetString(Prefs_Password, value);
        }
        
        public static string NickName
        {
            get => EditorPrefs.GetString(Prefs_NickName, "");
            set => EditorPrefs.SetString(Prefs_NickName, value);
        }
        
        public static string LoginTime
        {
            get => EditorPrefs.GetString(Prefs_LoginTime, "");
            set => EditorPrefs.SetString(Prefs_LoginTime, value);
        }

        public static JAuth Auth
        {
            get
            {
                var json = EditorPrefs.GetString(Prefs_Auth, "");
                return JsonConvert.DeserializeObject<JAuth>(json);
            }
            set
            {
                var json = JsonConvert.SerializeObject(value);
                EditorPrefs.SetString(Prefs_Auth, json);
            }
        }
        public static bool Admin
        {
            get => EditorPrefs.GetBool(Prefs_Admin, false);
            set => EditorPrefs.SetBool(Prefs_Admin, value);
        }

        public static LoginState LoginState
        {
            get => (LoginState)EditorPrefs.GetInt(Prefs_LoginState, (int)LoginState.Logout);
            set => EditorPrefs.SetInt(Prefs_LoginState, (int)value);
        }
    }
}