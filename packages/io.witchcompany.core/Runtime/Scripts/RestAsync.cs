using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Proyecto26;
using UnityEngine;
using UnityEngine.Networking;

namespace WitchCompany.Core.Runtime
{
    public class RestAsync
    {
        public static UniTask<T> Request<T>(RequestHelper helper, bool enableDebug = false)
        {
            var result = new UniTaskCompletionSource<T>();

            helper.IgnoreHttpException = true;
            helper.EnableDebug = false;

            Log(enableDebug, $"[Request] {helper.Uri} , {helper.BodyString}");

            RestClient.Request(helper)
                .Then(response =>
                {
                    Log(enableDebug, $"[Response] {helper.Uri} , {response.Text}");

                    if (!result.TrySetResult(JsonConvert.DeserializeObject<T>(response.Text))) 
                        throw new Exception("TrySetResult Failed");
                })
                .Catch(e => result.TrySetException(e));

            return result.Task;
        }
        
        public static UniTask<string> RequestRaw(RequestHelper helper, bool enableDebug = false)
        {
            var result = new UniTaskCompletionSource<string>();

            helper.IgnoreHttpException = true;
            helper.EnableDebug = false;

            Log(enableDebug, $"[Request] {helper.Uri} , {helper.BodyString}");

            RestClient.Request(helper)
                .Then(response =>
                {
                    Log(enableDebug, $"[Response] {helper.Uri} , {response.Text}");

                    if (!result.TrySetResult(response.Text)) 
                        throw new Exception("TrySetResult Failed");
                })
                .Catch(e => result.TrySetException(e));

            return result.Task;
        }
        
        public static async UniTask<Texture2D> RequestTexture(string imageURL)
        {
            using var req = UnityWebRequestTexture.GetTexture(imageURL);
            req.timeout = 10;

            await req.SendWebRequest();
                
            if (req.result == UnityWebRequest.Result.Success)
                return ((DownloadHandlerTexture) req.downloadHandler).texture;
                
            throw new Exception(req.error);
        }

        public static async UniTask<byte[]> RequestBytes(string url, int timeout = 5)
        {
            using var req = UnityWebRequest.Get(url);
            req.timeout = timeout;

            await req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
                return req.downloadHandler.data;
            else
                throw new Exception(req.error);
        }
        
        [System.Diagnostics.Conditional("ENABLE_DEBUG"), 
         System.Diagnostics.Conditional("UNITY_EDITOR")]
        private static void Log(bool enable, string msg)
        {
            if (enable) Debug.Log(msg);
        }
    }
}