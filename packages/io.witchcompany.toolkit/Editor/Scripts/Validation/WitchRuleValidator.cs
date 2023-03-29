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
            
            // 타겟 씬이 없으면 실패
            if (option.TargetScene == null) 
                return prefix + "타겟 씬이 없음";
            
            // 블록 이름 유효성 검사
            if (!Regex.IsMatch(option.Key, AssetBundleConfig.ValidNameRegex)) 
                return prefix + $"블록 이름 규칙 위반({option.Key}) -> 소문자로 시작하는, 소문자/숫자/언더바로 구성된 12자 이내의 문자열만 가능합니다.";

            return null;
        }
    }
}