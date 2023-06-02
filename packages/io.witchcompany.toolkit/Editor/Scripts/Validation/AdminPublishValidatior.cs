using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.GUI;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class AdminPublishValidatior
    {
        public static ValidationReport ValidationCheck()
        {
            return new ValidationReport()
                .Append(ValidatePathName())
                .Append(ValidateBlockName(AdminConfig.BlockNameKr, true))
                .Append(ValidateBlockName(AdminConfig.BlockNameEn, false))
                .Append(ValidateUnityKey());
        }
        
        private static ValidationReport ValidatePathName()
        {
            var report = new ValidationReport();
            var pathName = string.IsNullOrEmpty(AdminConfig.PathName) ? "" : AdminConfig.PathName;
            
            if (!Regex.IsMatch(pathName, AssetBundleConfig.ValidNameRegex))
            {
                var error = new ValidationError("path name은 소문자로 시작하고 소문자, 숫자, 언더바(_)만 사용할 수 있습니다. 1~20자 이내여야 합니다.");
                report.Append(error);
            }

            return report;
        }

        private static ValidationReport ValidateBlockName(string text, bool isKr)
        {
            var report = new ValidationReport();
            var blockName = string.IsNullOrEmpty(text) ? "" : text;
            var language = isKr ? "한글" : "영문";
            
            if (!Regex.IsMatch(blockName, AssetBundleConfig.ValidBlockNameRegex))
            {
                var error = new ValidationError($"block name({language})은 1~20자 이내여야 합니다.");
                report.Append(error);
            }

            return report;
        }
        
        
        private static ValidationReport ValidateUnityKey()
        {
            var report = new ValidationReport();
            var unityKey = CustomWindowAdmin.unityKeys;
            
            if (unityKey == null)
            {
                var error = new ValidationError("선택된 unityKey가 없습니다.");
                report.Append(error);
            }

            return report;
        }

        public static WitchDataManager ValidateDataManager()
        {
            var dataManager = GameObject.FindObjectOfType<WitchDataManager>(true);

            return dataManager;
        }
    }
}