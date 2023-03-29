using System;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.Tool.API;


namespace WitchCompany.Toolkit.Editor
{
    public static class CustomWindowAuth
    {
        
        private static GUIStyle style = new GUIStyle();

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
                // case LoginState.None:
                //     DrawLogin();
                //     break;
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
            
            Debug.Log(loginState);
        }
        
        
        private static void DrawWait()
        {
            GUILayout.Label("Account", EditorStyles.boldLabel);
            
            GUILayout.Label("로그인 중...");
            
        }


        // 로그아웃 상태 (로그인 진행)
        private static void DrawLogin()
        {
            // ChangeGUIStyle();
            GUILayout.Label("Account", EditorStyles.boldLabel);
            
            EditorGUILayout.BeginVertical("box");
            
            
            AuthConfig.Email = EditorGUILayout.TextField("E-Mail", AuthConfig.Email);
            AuthConfig.Password = EditorGUILayout.TextField("Password", AuthConfig.Password);

            GUILayout.Space(10);

            if (GUILayout.Button("Login"))
            {
                // 로그인 대기 상태로 변경
                if (loginState == LoginState.None || loginState == LoginState.Logout )
                {
                    loginState = LoginState.Wait;
                }
                
                Login();
                
            }
            
            EditorGUILayout.EndVertical();
        }
        
        // 로그인 상태 (로그인 정보 띄우기)
        private static void DrawAuthInfo()
        {
            GUILayout.Label("Account", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            
            EditorGUILayout.LabelField("E-Mail", AuthConfig.Email);
            EditorGUILayout.LabelField("Nick Name", AuthConfig.NickName);
            EditorGUILayout.LabelField("Login Time", AuthConfig.LoginTime);
            
            if (GUILayout.Button("Logout"))
            {
                Logout();
            }
            
            EditorGUILayout.EndVertical();
        }
        
        private static async void Login()
        {
            loginState = LoginState.Wait;
            
            var auth = await WitchAPI.Login(AuthConfig.Email, AuthConfig.Password);
            AuthConfig.Auth.accessToken = auth.accessToken;
            AuthConfig.Auth.refreshToken = auth.refreshToken;
                
            // 닉네임
            var response = await WitchAPI.GetUserInfo();

            // 로그인 상태로 변경
            if (response != null)
            {
                AuthConfig.NickName = response.profile.nickname;
                AuthConfig.LoginTime = DateTime.Now.ToString();
                
                loginState = LoginState.Login;
            }
            else
            {
                loginState = LoginState.Logout;
            }
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