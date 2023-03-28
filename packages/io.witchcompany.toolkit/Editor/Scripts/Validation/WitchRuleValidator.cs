using System.Text.RegularExpressions;
using WitchCompany.Toolkit.Editor.Configs;

namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class WitchRuleValidator
    {
        /// <summary>
        /// 위치월드 블록 룰 관련 유효성 검사
        /// - 블록 옵션 체크
        /// - 블록매니저가 최상위 오브젝트인지 검증
        /// - 각 WitchBehaviour 별 유효성 검증
        /// </summary>
        public static ValidationReport ValidationCheck(BlockPublishOption option)
        {
            return new ValidationReport()
                .Append(ValidateBlockOption(option));
        }

        // 블록 옵션 체크
        private static string ValidateBlockOption(BlockPublishOption option)
        {
            const string prefix = "잘못된 블록 옵션: ";
            
            // 블록 이름이 없으면 실패
            // if (string.IsNullOrEmpty(option.Name.kr))
            //     return prefix + "빈 한글 이름";
            // if (string.IsNullOrEmpty(option.Name.en))
            //     return prefix + "잘못된 블록 옵션: 빈 영어 이름";
            
            // 블록 이름 유효성 검사
            if (!Regex.IsMatch(option.Key, AssetBundleConfig.ValidNameRegex)) 
                return prefix + $"블록 이름 규칙 위반({option.Key})";
            
            // TODO: [API] 중복된 블록이름 검사
            
            // 타겟 씬이 없으면 실패
            if (option.TargetScene == null) 
                return prefix + "타겟 씬이 없음";
            // 씬 이름과 블록이름(영문) 이 다르면 실패 
            if (option.TargetScene.name != option.Key) 
                return prefix + $"씬 이름이 블록 이름과 다름. 씬({option.TargetScene.name}) 블록({option.Key})";

            return null;
        }
    }
}