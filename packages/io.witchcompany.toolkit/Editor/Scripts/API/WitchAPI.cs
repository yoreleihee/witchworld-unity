using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
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

            return !response.success ? null : response.payload;
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
            Log("토큰 리프래쉬 요청");
            
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

        public static async UniTask<bool> UploadBlock(BlockPublishOption option, JManifest manifest)
        {
            var auth = AuthConfig.Auth;
            if (string.IsNullOrEmpty(auth?.accessToken)) return false;
            
            var bundlePath = Path.Combine(AssetBundleConfig.BundleExportPath, option.BundleKey);
            var thumbnailPath = Path.Combine(AssetBundleConfig.BundleExportPath, option.ThumbnailKey);
            
            var bundleData = await File.ReadAllBytesAsync(bundlePath);
            var thumbnailData = await File.ReadAllBytesAsync(thumbnailPath);
            var form = new List<IMultipartFormSection>
            {
                //new MultipartFormDataSection("json", body, "application/json"),
                new MultipartFormFileSection("file1", bundleData, option.BundleKey, "application/octet-stream"),
                new MultipartFormFileSection("file2", thumbnailData, option.ThumbnailKey, "image/jpg")
            };
            //
            // var response = await Request<JAuth>(new RequestHelper
            // {
            //     Method = "POST",
            //     Uri = ApiConfig.URL("toolkit/create"),
            //     Headers = ApiConfig.TokenHeader(auth.refreshToken),
            //     BodyString = JsonConvert.SerializeObject(new Dictionary<string, string>
            //     {
            //         ["accessAt"] = "world"
            //     }),
            //     ContentType = ApiConfig.ContentType.Json
            // });
            //
            //return response.success ? response.payload : null;

            return false;
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
                    LogErr("토큰 리프래쉬 실패");
                    return res;
                }
                // 성공
                else
                {
                    Log("토큰 리프래쉬 성공");
                    
                    // 토큰 저장
                    AuthConfig.Auth = auth;
                    
                    // 요청 재시도
                    helper.Headers = ApiConfig.TokenHeader(auth.accessToken);
                    res = await Request<T>(helper);
                }
            }
            // 만료가 아닐 경우,
            return res;
        }

        private static async UniTask<JResponse<T>> Request<T>(RequestHelper helper)
        {
            using var request = new UnityWebRequest();
            
            try
            {
                // 리퀘스트 생성
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

                // 로그
                Log($"{helper.Method} Request ({helper.Uri})\n" + $"{helper.BodyString}");

                // 웹 리퀘스트 전송
                await request.SendWebRequest();

                // 예외처리
                if (request.result != UnityWebRequest.Result.Success || string.IsNullOrEmpty(request.downloadHandler?.text)) 
                    throw new UnityWebRequestException(request);
               
                // 성공
                Log($"{helper.Method} Response ({helper.Uri})\n" + $"{request.downloadHandler?.text}");
                return JsonConvert.DeserializeObject<JResponse<T>>(request.downloadHandler.text);
            }
            catch (Exception)
            {
                LogErr($"{helper.Method} Response ({helper.Uri})\n" + $"Failed: {request.error}");
                
                return new JResponse<T>
                {
                    message = request.error,
                    statusCode = (int)request.responseCode,
                    success = false
                };
            }
        }

        private static void Log(string msg)
        {
            if(ToolkitConfig.CurrLogLevel.HasFlag(LogLevel.API))
                Debug.Log(msg);
        }
        
        private static void LogErr(string msg)
        {
            if(ToolkitConfig.CurrLogLevel.HasFlag(LogLevel.API))
                Debug.LogError(msg);
        }
    }
}