using System;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.API;

namespace WitchCompany.Toolkit.Editor.GUI
{
    public static class CustomWindowAuth
    {
        public static string email;
        public static string password;
        
        private enum LoginState
        {
            None,
            Wait,
            Login,
            Logout
        }

        private static LoginState loginState = LoginState.Logout;
        
        public static void ShowAuth()
        {
            switch (loginState)
            {
                case LoginState.Wait:
                    DrawWait();
                    break;
                case LoginState.Login:
                    DrawAuthInfo();
                    break;
                case LoginState.Logout:
                    DrawLogin();
                    break;
            }
        }
        
        private static void DrawWait()
        {
            GUILayout.Label("Account", EditorStyles.boldLabel);
            
            GUILayout.Label("로그인 중...");
            
        }

        // 로그아웃 상태 (로그인 진행)
        private static void DrawLogin()
        {
            GUILayout.Label("Account", EditorStyles.boldLabel);
            
            EditorGUILayout.BeginVertical("box");
            
            email = EditorGUILayout.TextField("E-Mail", email);
            password = EditorGUILayout.PasswordField("Password",password);
            

            EditorGUILayout.EndVertical();

            if (GUILayout.Button("Login"))
            {
                AuthConfig.Email = email;
                AuthConfig.Password = password;
                Login();
            }
            
        }
        
        // 로그인 상태 (로그인 정보 띄우기)
        private static void DrawAuthInfo()
        {
            GUILayout.Label("Account", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            
            EditorGUILayout.LabelField("E-Mail", AuthConfig.Email);
            EditorGUILayout.LabelField("Nick Name", AuthConfig.NickName);
            EditorGUILayout.LabelField("Login Time", AuthConfig.LoginTime);
            
            EditorGUILayout.EndVertical();
            if (GUILayout.Button("Logout"))
            {
                Logout();
            }
            
        }
        
        private static async void Login()
        {
            loginState = LoginState.Wait;
            
            var auth = await WitchAPI.Login(AuthConfig.Email, AuthConfig.Password);
            
            if (auth == null)
            {
                Debug.Log("회원 정보가 없습니다.");
                loginState = LoginState.Logout;
                return;
            }
            // 토큰
            AuthConfig.Auth = auth;

            var response = await WitchAPI.GetUserInfo();

            if (response == null)
            {
                loginState = LoginState.Logout;
                return;
            }
            AuthConfig.NickName = response.profile.nickname;
            AuthConfig.LoginTime = DateTime.Now.ToString();
            loginState = LoginState.Login;
            
            EditorWindow.focusedWindow.Repaint();
        }

        private static void Logout()
        {
            WitchAPI.Logout();
            
            // 로그인 상태로 변경
            if (loginState == LoginState.Login)
            {
                loginState = LoginState.Logout;
            }
        }
    }
}