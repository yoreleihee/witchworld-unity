using UnityEditor;

namespace WitchCompany.Toolkit.Editor.Configs
{
    /// <summary>
    /// 툴킷을 통해 로그인한 정보
    /// </summary>
    public class AuthConfig
    {
        private const string Prefs_Email = "email";
        private const string Prefs_Password = "password";
        private const string Prefs_NickName = "nick_name";
        private const string Prefs_LoginTime = "login_time";
        private const string Prefs_AccessToken = "access_token";
        private const string Prefs_RefreshToken = "refresh_token";


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
        
        public static string AccessToken
        {
            get => EditorPrefs.GetString(Prefs_AccessToken, "");
            set => EditorPrefs.SetString(Prefs_AccessToken, value);
        }
        
        public static string RefreshToken
        {
            get => EditorPrefs.GetString(Prefs_RefreshToken, "");
            set => EditorPrefs.SetString(Prefs_RefreshToken, value);
        }
    }
}