using Cysharp.Threading.Tasks;
using UnityEngine;
using WitchCompany.Toolkit.Editor.API;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class AwaitValidator
    {
        public static async UniTask<ValidationReport> ValidationCheck()
        {
            return new ValidationReport()
                .Append(await ValidateHasBlock());
        }

        // 블록 존재 여부 확인
        private static async UniTask<ValidationReport> ValidateHasBlock()
        {
            var report = new ValidationReport();
            var portals = GameObject.FindObjectsOfType<WitchPortal>();
            var privatePortals = GameObject.FindObjectsOfType<WitchPrivatePortal>();
            
            // 포탈
            foreach (var portal in portals)
            {
                // 패스네임으로 블록 조회 
                var result = await WitchAPI.CheckExistBlock(portal.TargetUrl);
        
                if (result is not { isExist: true })
                {
                    var error = new ValidationError(
                        $"Object : {portal.gameObject.name}\ntargetUrl {portal.TargetUrl}은 존재하지 않는 블록입니다.",  ValidationTag.TagPortal, portal);
                    report.Append(error);
                }
            }
        
            // 프라이빗 포탈
            foreach (var privatePortal in privatePortals)
            {
                var result = await WitchAPI.CheckExistBlock(privatePortal.TargetUrl);
        
                if (result is not { isExist: true })
                {
                    var error = new ValidationError(
                        $"Object : {privatePortal.gameObject.name}\ntargetUrl {privatePortal.TargetUrl}은 존재하지 않는 블록입니다.", ValidationTag.TagPortal, privatePortal);
                    report.Append(error);
                }
            }
        
            return report;
        }
    }
}