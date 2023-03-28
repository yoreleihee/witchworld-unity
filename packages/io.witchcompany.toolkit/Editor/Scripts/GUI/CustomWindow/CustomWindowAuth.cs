using System;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;


namespace WitchCompany.Toolkit.Editor
{
    public static class CustomWindowAuth
    {
        // private static int testInt;
        // private static bool isLogin;
        //
        // private static string eMail;
        // private static string password;
        // private static string acToken;
        // private static string rfToken;
        // private static string nickName;
        // private static string loginTime;
        
        
        private static GUIStyle style = new GUIStyle();
        
        
        public static async void ShowAuth()
        {
            // 로그인 토근이 없으면 로그인 화면, 있으면 로그인한 정보 띄움
            if (string.IsNullOrEmpty(AuthConfig.Auth.accessToken))
            {
                DrawLogin();
            }
            else
            {
                DrawAuthInfo();
            }
        }


        // 로그아웃 상태 (로그인 진행)
        private static void DrawLogin()
        {
            // ChangeGUIStyle();
            GUILayout.Label("Account", EditorStyles.boldLabel);
            GUILayout.Space(10);
            
            // password = EditorGUILayout.TextField("Password", password);
            // acToken = EditorGUILayout.TextField("Access Token", acToken);
            // rfToken = EditorGUILayout.TextField("Refresh Token", rfToken);
            
            AuthConfig.Email = EditorGUILayout.TextField("E-Mail", AuthConfig.Email);
            AuthConfig.Password = EditorGUILayout.TextField("Password", AuthConfig.Password);
            GUILayout.Space(5);

            // EditorGUILayout.LabelField("Access Token", AuthConfig.AccessToken);
            // EditorGUILayout.LabelField("Refresh Token", AuthConfig.RefreshToken);
            
            GUILayout.Space(10);

            if (GUILayout.Button("Login"))
            {
                Login();
            }
        }
        
        // 로그인 상태 (로그인 정보 띄우기)
        private static void DrawAuthInfo()
        {
            EditorGUILayout.LabelField("E-Mail", AuthConfig.Email);
            EditorGUILayout.LabelField("Nick Name", AuthConfig.NickName);
            EditorGUILayout.LabelField("Login Time", AuthConfig.LoginTime);
            
            if (GUILayout.Button("Logout"))
            {
                Logout();
            }
        }
        
        private static async void Login()
        {
            Debug.Log("login click!");
            //var auth = await AuthAPI.EmailLogin(eMail,password);

            // var auth = await AuthAPI.EmailLogin(eMail, password);
            // acToken = auth.accessToken;
            // rfToken = auth.refreshToken;
            //
            // Debug.Log(JsonConvert.SerializeObject(auth));
            //     
            // // 닉네임
            // var response = await UserAPI.GetUser(acToken);
            // nickName = response.profile.nickname;

            // 임시
            // AuthConfig.AccessToken = "temp_access_token";
            // AuthConfig.RefreshToken = "";
            AuthConfig.NickName = "temp_nickname";
            AuthConfig.LoginTime = DateTime.Now.ToString();
        
        }

        private static async void Logout()
        {
            AuthConfig.Email = null;
            AuthConfig.Password = null;
            // AuthConfig.AccessToken = null;
            // AuthConfig.RefreshToken = null;
            AuthConfig.NickName = null;
            AuthConfig.LoginTime = null;
            
            Debug.Log("logout click!");
            
        }
        
        
        private static async UniTask TestUniTask()
        {
            await UniTask.Delay(1000);
            Debug.Log("대기 1초");
            await UniTask.Delay(1000);
            Debug.Log("대기 2초");
            await UniTask.Delay(1000);
            Debug.Log("대기 3초");
        }

        private static void ChangeGUIStyle(int fontSize = 12, Color fontColor = default)
        {
            if(fontColor == default) fontColor = Color.white;
            
            style.fontSize = fontSize;
            style.normal.textColor = fontColor;
        }
        
    }
}