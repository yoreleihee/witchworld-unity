using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEditor.PackageManager;
using WitchCompany.Toolkit.Editor.Configs;

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
        public static async UniTask<PackageInfo> GePackageInfo(string name)
        {
            var listRequest = Client.List(true);
            await UniTask.WaitUntil(() => listRequest.IsCompleted);
            
            return listRequest.Result.FirstOrDefault(p => p.name == name);
        }
    }
}