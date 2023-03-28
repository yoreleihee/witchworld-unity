using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Proyecto26;
using UnityEngine;
using UnityEngine.Networking;
using WitchCompany.Core.Runtime;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.Configs.Enum;
using WitchCompany.Toolkit.Editor.DataStructure;

namespace WitchCompany.Toolkit.Editor.Tool.API
{
    public static partial class WitchAPI
    {
        public static async UniTask<JAuth> Login(string email, string password)
        {
            var response = await Request<JAuth>(new RequestHelper
            {
                Method = "POST",
                Uri = ApiConfig.URL("user/login/ww"),
                BodyString = JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    ["accessAt"] = "world",
                    ["blockName"] = "",
                    ["ip"] = "unity_editor",
                    ["email"] = email,
                    ["password"] = password
                }),
                ContentType = ApiConfig.ContentType.Json
            });

            if (!response.success) return null;
            
            AuthConfig.Auth = response.payload;
            return response.payload;
        }
        
        public static void Logout()
        {
            AuthConfig.Auth = new JAuth();
        }

        public static async UniTask<JUserInfo> GetUserInfo()
        {
            var auth = AuthConfig.Auth;
            if (string.IsNullOrEmpty(auth?.accessToken)) return null;
            
            var response = await AuthSafeRequest<JUserInfo>(new RequestHelper
            {
                Method = "POST",
                Uri = ApiConfig.URL("user/auth/unity"),
                Headers = ApiConfig.TokenHeader(auth.accessToken)
            });

            return response.success ? response.payload : null;
        }

        public static async UniTask<JAuth> Refresh()
        {
            var auth = AuthConfig.Auth;
            
            if (string.IsNullOrEmpty(auth.refreshToken))
                return null;
            
            var response = await Request<JAuth>(new RequestHelper
            {
                Method = "POST",
                Uri = ApiConfig.URL("user/refresh/ww"),
                Headers = ApiConfig.TokenHeader(auth.refreshToken),
                BodyString = JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    ["accessAt"] = "world"
                }),
                ContentType = ApiConfig.ContentType.Json
            });

            return response.success ? response.payload : null;
        }
    }
    
    public static partial class WitchAPI
    {
        private static async UniTask<JResponse<T>> AuthSafeRequest<T>(RequestHelper helper)
        {
            var res = await Request<T>(helper);
            
            // 토큰 만료일 경우,
            if (res.statusCode == 401)
            {
                var auth = await Refresh();
                
                // 실패
                if (auth == null)
                {
                    Debug.Log("토큰 리프래쉬 실패");
                    return res;
                }
                // 성공
                else
                {
                    Debug.Log("토큰 리프래쉬 성공");
                    
                    // 토큰 저장
                    AuthConfig.Auth = auth;
                    
                    // 요청 재시도
                    res = await Request<T>(helper);
                }
            }
            // 만료가 아닐 경우,
            return res;
        }

        private static async UniTask<JResponse<T>> Request<T>(RequestHelper helper)
        {
            using var request = new UnityWebRequest();
            
            request.method = helper.Method;
            request.url = helper.Uri;
            foreach (var (key, value) in helper.Headers) 
                request.SetRequestHeader(key, value);
            request.downloadHandler = new DownloadHandlerBuffer();
            if (!string.IsNullOrEmpty(helper.BodyString))
            {
                var bytes = Encoding.UTF8.GetBytes(helper.BodyString.ToCharArray());
                request.uploadHandler = new UploadHandlerRaw(bytes);

                var contentType = string.IsNullOrEmpty(helper.ContentType)
                    ? ApiConfig.ContentType.Json
                    : helper.ContentType; 
                request.uploadHandler.contentType = contentType;
            }

            var baseRequestLog = $"{helper.Method} Request ({helper.Uri})\n";
            
            if(ToolkitConfig.CurrLogLevel.HasFlag(LogLevel.API))
                Debug.Log( baseRequestLog + $"{helper.BodyString}");

            await request.SendWebRequest();
            
            if (ToolkitConfig.CurrLogLevel.HasFlag(LogLevel.API))
            {
                if(request.result == UnityWebRequest.Result.Success)
                    Debug.Log(baseRequestLog + $"{request.downloadHandler?.text}");
                else
                    Debug.LogError(baseRequestLog + $"Failed: {request.error}");
            }

            if (request.result == UnityWebRequest.Result.Success)
                return JsonConvert.DeserializeObject<JResponse<T>>(request.downloadHandler.text);
            else
                return null;
        }
    }
}