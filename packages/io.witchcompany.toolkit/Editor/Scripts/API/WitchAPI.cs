using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.Tool;

namespace WitchCompany.Toolkit.Editor.API
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

        /// <summary>
        /// 1성공, -1로그인 필요, -2권한 필요
        /// </summary>
        public static async UniTask<int> CheckPermission()
        {
            var auth = AuthConfig.Auth;
            if (string.IsNullOrEmpty(auth?.accessToken)) return -1;
            
            var response = await AuthSafeRequest<JPermission>(new RequestHelper
            {
                Method = "GET",
                Uri = ApiConfig.URL("v2/toolkits/permission"),
                Headers = ApiConfig.TokenHeader(auth.accessToken)
            });

            if (response.success && response.payload != null)
                return response.payload.isUploadAble ? 1 : -2;
            
            return -2;
        }

        public static async UniTask<bool> UploadBlock(BlockPublishOption option, JManifest manifest)
        {
            var auth = AuthConfig.Auth;
            if (string.IsNullOrEmpty(auth?.accessToken)) return false;
            
            var bundlePath = Path.Combine(AssetBundleConfig.BundleExportPath, option.BundleKey);
            var thumbnailPath = Path.Combine(AssetBundleConfig.BundleExportPath, option.ThumbnailKey);
            var bundleData = await File.ReadAllBytesAsync(bundlePath);
            var thumbnailData = await File.ReadAllBytesAsync(thumbnailPath);

            var body = new JPublish
            {
                block = new JBlock
                {
                    name = option.Key,
                    theme = option.theme.ToString()
                },
                manifest = manifest
            };

            var json = JsonConvert.SerializeObject(body);

            var form = new List<IMultipartFormSection>
            {
                new MultipartFormDataSection("json", json, "application/json"),
                new MultipartFormFileSection("bundle", bundleData, option.BundleKey, ""),
                new MultipartFormFileSection("image", thumbnailData, option.ThumbnailKey, "image/jpg")
            };
            
            Debug.Log("토큰 : " + auth.accessToken);
            Debug.Log("json : " + json);
            
            
            var response = await Request<JPublishResponse>(new RequestHelper
            {
                Method = "POST",
                Uri = ApiConfig.URL("v2/toolkits/unity"),
                Headers = ApiConfig.TokenHeader(auth.accessToken),
                FormSections = form
            });
            
            Debug.Log("message : " + response.message);
            return response.success;
        }
        
        public static async UniTask<JAuthBlockInfo> RegisterBlock(BlockPublishOption option, JLanguageString blockName)
        {
            var auth = AuthConfig.Auth;
            if (string.IsNullOrEmpty(auth?.accessToken)) return null;
            var thumbnailPath = Path.Combine(AssetBundleConfig.BundleExportPath, option.ThumbnailKey);
            var thumbnailData = await File.ReadAllBytesAsync(thumbnailPath);  // todo : 에디터에서 파일 선택
            
            var body = new JRegisterBlockData
            {
                // 임시 데이터
                unityKeyId = EditorTest.UnitykeyId,
                type = BlockType.Community.ToString(),
                theme = option.theme.ToString(),
                createUserNickname = AuthConfig.NickName,
                name = blockName
            };
            
            var form = new List<IMultipartFormSection>
            {
                new MultipartFormDataSection("json", JsonConvert.SerializeObject(body), "application/json"),
                new MultipartFormFileSection("image", thumbnailData, option.ThumbnailKey, "image/jpg")
            };

            var response = await Request<JAuthBlockInfo>(new RequestHelper
            {
                Method = "POST",
                Uri = ApiConfig.URL("v2/toolkits/blocks"),
                Headers = ApiConfig.TokenHeader(auth.accessToken),
                FormSections = form
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

        private static string GetFormSectionsContentType(out byte[] bodyRaw, RequestHelper options)
        {
            var boundary = UnityWebRequest.GenerateBoundary();
            var formSections = UnityWebRequest.SerializeFormSections(options.FormSections, boundary);
            var terminate = Encoding.UTF8.GetBytes(string.Concat("\r\n--", Encoding.UTF8.GetString(boundary), "--"));
            
            bodyRaw = new byte[formSections.Length + terminate.Length];
            
            Buffer.BlockCopy(formSections, 0, bodyRaw, 0, formSections.Length);
            Buffer.BlockCopy(terminate, 0, bodyRaw, formSections.Length, terminate.Length);
            
            return string.Concat("multipart/form-data; boundary=", Encoding.UTF8.GetString(boundary));
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

                var contentType = ApiConfig.ContentType.Json;
                var bodyRaw = helper.BodyRaw;
                
                if (!string.IsNullOrEmpty(helper.BodyString))
                {
                    bodyRaw = Encoding.UTF8.GetBytes(helper.BodyString.ToCharArray());

                    contentType = string.IsNullOrEmpty(helper.ContentType)
                        ? ApiConfig.ContentType.Json
                        : helper.ContentType;
                    
                    Log($"{helper.Method} Request ({helper.Uri})\n" + $"{contentType}\n{helper.BodyString}");
                }
                else if (helper.FormSections is {Count: > 0})
                {
                    contentType = GetFormSectionsContentType(out bodyRaw, helper);

                    var builder = new StringBuilder();
                    builder.Append($"{helper.Method} Request ({helper.Uri})\n");
                    builder.Append($"{contentType}\n");
                    foreach (var section in helper.FormSections)
                    {
                        var sizeMb = CommonTool.ByteToMb(section.sectionData.LongLength, 4);
                        builder.Append($" {section.sectionName}: {section.fileName}({sizeMb}mb); {section.contentType}\n");
                    }

                    Log(builder.ToString());
                }

                //request.SetRequestHeader("Content-Type", contentType);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.uploadHandler.contentType = contentType;

                // 웹 리퀘스트 전송
                await request.SendWebRequest();

                // 예외처리
                if (request.result != UnityWebRequest.Result.Success || string.IsNullOrEmpty(request.downloadHandler?.text)) 
                    throw new UnityWebRequestException(request);
               
                // 성공
                Log($"{helper.Method} Response ({helper.Uri})\n" + $"{request.downloadHandler?.text}");
                return JsonConvert.DeserializeObject<JResponse<T>>(request.downloadHandler.text);
            }
            catch (Exception e)
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