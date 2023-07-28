using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;
using Progress = Cysharp.Threading.Tasks.Progress;

namespace WitchCompany.Toolkit.Editor.Tool
{
    /// <summary>
    /// 버전을 관리해주는 도구.
    /// TODO: 성범 -> 현재 툴킷 버전 가져오기
    /// TODO: 성범 -> 깃에 있는 최신 툴킷 버전 가져오기
    /// TODO: 성범 -> 자동으로 최신 버전 업데이트
    /// </summary>
    public static class PackageTool
    {
        private static ListRequest Request;
        private static bool isRequesting = false;
        
        public static string GePackageInfo()
        {
            if(isRequesting) return "0.1.72";

            Debug.Log("Request!");
            
            isRequesting = true;
            Request = Client.List(true);    // List packages installed for the project
            EditorApplication.update += Progress;

            return "0.1.72";
            // PackageInfo packageInfo = Client.("");
            // Debug.Log("Package version: " + packageInfo.version);
            //
            // //Client.Search()
            //
            // var listRequest = Client.List(true);
            // await UniTask.WaitUntil(() => listRequest.IsCompleted);
            //
            // return listRequest.Result.FirstOrDefault(p => p.name == name);
        }

        public static void Progress()
        {
            if (Request == null)
            {
                isRequesting = false;
                return;
            }

            if (Request.IsCompleted)
            {
                if (Request.Status == StatusCode.Success)
                {
                    foreach (var package in Request.Result)
                    {
                        if(package.name != "io.witchcompany.toolkit")
                             continue;
                        
                        var str =
                            $"Package name: {package.name}\n" +
                            $"Package id: {package.packageId}\n" +
                            $"Package version: {package.version}\n" +
                            $"lateset Versions: {package.versions.latest}\n" +
                            $"lateset Versions: {package.versions.latestCompatible}\n" +
                            $"lateset Versions: {package.versions.verified}\n";
                        Debug.Log(str);
                    }
                }
                else if (Request.Status >= StatusCode.Failure)
                    Debug.LogError(Request.Error.message);

                EditorApplication.update -= Progress;
            }

            isRequesting = false;
        }
    }
}