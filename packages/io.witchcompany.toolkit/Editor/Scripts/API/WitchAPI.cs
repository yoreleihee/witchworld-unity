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
using WitchCompany.Toolkit.Editor.DataStructure.Admin;
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

        /// <summary> 파일 경로 존재할 때 파일 반환 </summary>
        private static async UniTask<byte[]> GetByte(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            
            var bytes = await File.ReadAllBytesAsync(filePath);
            return bytes;
        }
        
        /// <summary>
        /// 유니티 키 생성 (번들 업로드)
        /// 성공(1), 실패(-1), pathName 중복(-2)
        /// </summary>
        public static async UniTask<int> UploadBundle(BlockPublishOption option, Dictionary<string, JManifest> manifests)
        {
            var auth = AuthConfig.Auth;
            if (string.IsNullOrEmpty(auth?.accessToken)) return -1;
            
            // bundle
            var standaloneBundlePath = Path.Combine(AssetBundleConfig.BundleExportPath, AssetBundleConfig.Standalone, option.BundleKey);
            var webglBundlePath = Path.Combine(AssetBundleConfig.BundleExportPath, AssetBundleConfig.WebGL, option.BundleKey);
            var androidBundlePath = Path.Combine(AssetBundleConfig.BundleExportPath, AssetBundleConfig.Android, option.BundleKey);
            var iosBundlePath = Path.Combine(AssetBundleConfig.BundleExportPath, AssetBundleConfig.Ios, option.BundleKey);

            var standaloneBundleData = await GetByte(standaloneBundlePath);
            var webglBundleData = await GetByte(webglBundlePath);
            var androidBundleData = await GetByte(androidBundlePath);
            var iosBundleData = await GetByte(iosBundlePath);
            
            // thumbnail
            var thumbnailPath = Path.Combine(AssetBundleConfig.BundleExportPath, option.ThumbnailKey);
            var thumbnailData = await File.ReadAllBytesAsync(thumbnailPath);
            
            var blockData = new JBlock
            {
                pathName = option.Key,
                theme = option.theme.ToString().ToLower()
            };

            var bundleData = new JBundle
            {
                blockData = blockData,
                standalone = new JBundleData{manifest = manifests[AssetBundleConfig.Standalone]},
                webgl = new JBundleData{manifest = manifests[AssetBundleConfig.WebGL]},
                android = new JBundleData{manifest = manifests[AssetBundleConfig.Android]},
                ios = new JBundleData{manifest = manifests[AssetBundleConfig.Ios]}
            };

            var jsonBundleData = JsonConvert.SerializeObject(bundleData);
            
            var form = new List<IMultipartFormSection>
            {
                new MultipartFormDataSection("json", jsonBundleData, "application/json"),
                new MultipartFormFileSection("image", thumbnailData, option.ThumbnailKey, "image/jpg"),
            };
            
            // 번들 파일 있을 때만 보냄
            if(standaloneBundleData != null)
                form.Add(new MultipartFormFileSection(AssetBundleConfig.Standalone, standaloneBundleData, option.BundleKey, ""));
            if(webglBundleData != null)
                form.Add(new MultipartFormFileSection(AssetBundleConfig.WebGL, webglBundleData, option.BundleKey, ""));
            if(androidBundleData != null)
                form.Add(new MultipartFormFileSection(AssetBundleConfig.Android, androidBundleData, option.BundleKey, ""));
            if(iosBundleData != null)
                form.Add(new MultipartFormFileSection(AssetBundleConfig.Ios, iosBundleData, option.BundleKey, ""));
             
            var response = await Request<JPublishResponse>(new RequestHelper
            {
                Method = "POST",
                Uri = ApiConfig.URL("v2/toolkits/unity-key"),
                Headers = ApiConfig.TokenHeader(auth.accessToken),
                FormSections = form
            });

            if (response.statusCode == 200) return 1;
            if (response.statusCode == 409) return -2;
            return -1;
        }

        /// <summary> 유니티 키 리스트 조회 </summary>
        public static async UniTask<List<JUnityKey>> GetUnityKeys(int page, int limit)
        {
            var auth = AuthConfig.Auth;
            if (string.IsNullOrEmpty(auth?.accessToken)) return null;
            
            var response = await Request<List<JUnityKey>>(new RequestHelper
            {
                Method = "GET",
                Uri = ApiConfig.URL($"v2/toolkits/unity-key?page={page}&limit={limit}"),
                Headers = ApiConfig.TokenHeader(auth.accessToken)
            });
            
            return response.success ? response.payload : null;
        }
        
        /// <summary>
        /// 유니티 키로 블록 생성
        /// 성공(blockId), 실패(-1), pathName 중복(-2)
        /// </summary>
        public static async UniTask<int> UploadBlock(JBlockData blockData)
        {
            var auth = AuthConfig.Auth;
            if (string.IsNullOrEmpty(auth?.accessToken)) return -1;


            var thumbnailData = await GetByte(AdminConfig.ThumbnailPath);
            var thumbnailKey = AdminConfig.ThumbnailPath.Split("/")[^1];
            
            
            var form = new List<IMultipartFormSection> {
                new MultipartFormDataSection("json", JsonConvert.SerializeObject(blockData), "application/json"),
            };
            
            if(thumbnailData != null)
                form.Add(new MultipartFormFileSection("image", thumbnailData, thumbnailKey, "image/jpg"));
            
            var response = await Request<JBlockData>(new RequestHelper
            {
                Method = "POST",
                Uri = ApiConfig.URL("v2/toolkits/blocks"),
                Headers = ApiConfig.TokenHeader(auth.accessToken),
                FormSections = form
            });
            
            if (response.statusCode == 200) return response.payload.blockId;
            if (response.statusCode == 409) return -2;
            return -1;
        }
        
        /// <summary>
        /// 랭킹보드 keys 업로드
        /// </summary>
        /// <param name="rankingData"></param>
        /// <returns></returns>
        public static async UniTask<bool> SetRankingKeys(int blockId, List<JRankingKey> rankingKeys)
        {
            var auth = AuthConfig.Auth;
            if (string.IsNullOrEmpty(auth?.accessToken)) return false;

            var rankingData = new JRankingData
            {
                blockId = blockId,
                rankingKeys = rankingKeys
            };
            
            var response = await Request<JRankingData>(new RequestHelper
            {
                Method = "POST",
                Uri = ApiConfig.URL("v2/blocks/rank/keys"),
                Headers = ApiConfig.TokenHeader(auth.accessToken),
                BodyString = JsonConvert.SerializeObject(rankingData)
            });
            
            return response.success;
        }

        public static async UniTask<bool> CheckValidItem(int itemKey)
        {
            
            var response = await Request<JItemDetail>(new RequestHelper
            {
                Method = "GET",
                Uri = ApiConfig.URL($"item/detail/{itemKey}")
            });
            
            return response.success;
        }
        /// <summary>
        /// pathName으로 블록 조회 
        /// </summary>
        /// <param name="pathName"></param>
        /// <returns></returns>
        public static async UniTask<JBlockData> GetBlock(string pathName)
        {
            var response = await Request<JBlockData>(new RequestHelper
            {
                Method = "GET",
                Uri = ApiConfig.URL($"v2/toolkits/blocks/{pathName}")
            });

            return response.success ? response.payload : null;
        }
        
        /// <summary> 블록 존재 여부 조회 </summary>
        /// <param name="pathName"></param>
        /// <returns></returns>
        public static async UniTask<JExistBlock> CheckExistBlock(string pathName)
        {
            var result = await Request<JExistBlock>(new RequestHelper
            {
                Method = "GET",
                Uri = ApiConfig.URL($"v2/blocks/check/accessible/{pathName}")
            });

            return result.success ? result.payload : null;
        }

        public static async UniTask<bool> UpdateBlockStatus(string pathName)
        {
            var auth = AuthConfig.Auth;

            var blockStatusData = new JBlockStatus
            {
                pathname = pathName,
                status = (int)AdminConfig.BlockStatus
            };

            var response = await Request<JBlockStatus>(new RequestHelper
            {
                Method = "POST",
                Uri = ApiConfig.URL("v2/blocks/status/change"),
                Headers = ApiConfig.TokenHeader(auth.accessToken),
                BodyString = JsonConvert.SerializeObject(blockStatusData)
            });

             Debug.Log(JsonConvert.SerializeObject(blockStatusData));
            
            return response != null && response.success;
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
                        if (section.sectionName == "json")
                            builder.Append($"{section.sectionName}({section.contentType}):\n" + Encoding.UTF8.GetString(section.sectionData));
                        else
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