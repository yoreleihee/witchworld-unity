using System.Text.RegularExpressions;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.Tool;
using WitchCompany.Toolkit.Module;

namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class WitchRuleValidator
    {
        /// <summary>
        /// 위치월드 블록 룰 관련 유효성 검사
        /// - 블록 옵션 확인
        /// - 블록매니저가 최상위 오브젝트인지 확인
        /// - 각 WitchBehaviour 별 유효성 검증
        /// </summary>
        public static ValidationReport ValidationCheck(BlockPublishOption option)
        {
            return new ValidationReport()
                .Append(ValidateBlockOption(option))
                .Append(ValidateHierarchy(option));
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

        private static string ValidateHierarchy(BlockPublishOption option)
        {
            const string prefix = "잘못된 계층 구조: ";

            var scene = SceneManager.GetActiveScene();
            var rootObjects = scene.GetRootGameObjects();

            if (rootObjects.Length <= 0)
                return prefix + "씬이 비어있습니다. WitchBlockManager 가 필요합니다.";
            if (rootObjects.Length > 1)
                return prefix + "한 씬의 부모 오브젝트는 하나여야 합니다. WitchBlockManager가 부착된 오브젝트를 만들고, 그 하위로 이동시켜주세요.";
            if (!rootObjects[0].TryGetComponent<WitchBlockManager>(out _))
                return prefix + "최상위 오브젝트는 WitchBlockManager 가 부착되어야 합니다.";

            return null;
        }
    }
}