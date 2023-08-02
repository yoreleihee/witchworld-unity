using System;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.API;
using WitchCompany.Toolkit.Editor.DataStructure;

namespace WitchCompany.Toolkit.Editor.GUI
{
    public static class CustomWindowAuth
    {
        public static string email;
        public static string password;

        // private static LoginState loginState = LoginState.Logout;
        
        public static void ShowAuth()
        {
            switch (AuthConfig.LoginState)
            {
                case LoginState.Logout:
                    DrawLogin();
                    break;
                case LoginState.Wait:
                    DrawWait();
                    break;
                case LoginState.Login:
                    DrawAuthInfo();
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
            
            // if (ToolkitConfig.DeveloperMode)
            // {
            //     AuthConfig.DevEmail = EditorGUILayout.TextField("E-Mail", AuthConfig.DevEmail);
            //     AuthConfig.DevPassword = EditorGUILayout.PasswordField("Password", AuthConfig.DevPassword);
            // }
            // else
            // {
            //     AuthConfig.Email = EditorGUILayout.TextField("E-Mail", AuthConfig.Email);
            //     AuthConfig.Password = EditorGUILayout.PasswordField("Password", AuthConfig.Password);
            // }
            
            email = EditorGUILayout.TextField("E-Mail", email);
            password = EditorGUILayout.PasswordField("Password", password);
            
            
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
            AuthConfig.LoginState = LoginState.Wait;

            // 로그인
            // email = ToolkitConfig.DeveloperMode ? AuthConfig.DevEmail : AuthConfig.Email;
            // password = ToolkitConfig.DeveloperMode ? AuthConfig.DevPassword : AuthConfig.Password;
            
            var auth = await WitchAPI.Login(AuthConfig.Email, AuthConfig.Password);
            
            if (auth == null)
            {
                Debug.Log("회원 정보가 없습니다.");
                AuthConfig.LoginState = LoginState.Logout;
                return;
            }


            // 유저 정보
            var response = await WitchAPI.GetUserInfo(auth);

            if (response == null)
            {
                AuthConfig.LoginState = LoginState.Logout;
                return;
            }
            
            AuthConfig.Auth = auth;
            AuthConfig.NickName = response.profile.nickname;
            AuthConfig.LoginTime = DateTime.Now.ToString();
            AuthConfig.Admin = response.profile.role == 9;
            AuthConfig.LoginState = LoginState.Login;
            
            EditorWindow.focusedWindow.Repaint();
        }

        private static void Logout()
        {
            // 로그아웃 상태로 변경
            AuthConfig.LoginState = LoginState.Logout;
            AuthConfig.Auth = new JAuth();
            // AuthConfig.Email = "";
            // AuthConfig.Password = "";
            AuthConfig.NickName = "";
            AuthConfig.LoginTime = "";
            AuthConfig.Admin = false;
        }
        
    }
}