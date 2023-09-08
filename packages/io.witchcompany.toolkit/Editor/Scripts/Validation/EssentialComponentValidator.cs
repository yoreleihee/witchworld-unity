using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Validation;
namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class EssentialComponentValidator
    {
        private const string CommonText = "모든";
        
        // 모든 테마 필수 컴포넌트
        private static readonly string[] CommonEssentialComponents =
        {
            nameof(WitchBlockManager),
            // nameof(WitchBlockAssetPoint),
            //nameof(WitchPortal)
            //"WitchSpawnPoint"
        };
        
        // 게임 테마 필수 컴포넌트
        private static readonly string[] GameEssentialComponents =
        {
            nameof(WitchLeaderboard)
        };
        
        /// <summary> 필수 컴포넌트 검사 </summary>
        public static ValidationReport ValidationCheck()
        {
            var report = new ValidationReport();
            var allTransforms = GameObject.FindObjectsOfType<Transform>(true);
            
            var allComponentNames = new List<string>();
            foreach (var tr in allTransforms)
            {
                var trComponents = tr.GetComponents<Component>();

                foreach (var component in trComponents)
                {
                    var fullType = component.GetType();

                    if (fullType != null)
                    {
                        var type = fullType.FullName.Split(".")[^1];
                        allComponentNames.Add(type);
                    }
                }
            }

            // 공통
            NotEssentialComponent(report, allComponentNames.ToArray(), CommonEssentialComponents, CommonText);
            
            // 게임
            if(PublishConfig.Theme == BundleTheme.Game)
                NotEssentialComponent(report, allComponentNames.ToArray(), GameEssentialComponents, BundleTheme.Game.ToString());
            
            return report;
        }

        private static void NotEssentialComponent(ValidationReport report, string[] allComponents, string[] targetComponents, string theme = "")
        {
            foreach (var component in targetComponents)
            {
                var isExist = Array.Exists(allComponents, x => x == component);
                
                if(isExist) continue;

                // 공통 컴포넌트 존재하지 않을 경우
                var error = new ValidationError($"{theme} 블록에서 필수로 배치되어야 하는 {component}가 없습니다.",
                    ValidationTag.TagEssentialList);
                
                report.Append(error);
            }
        }
    }
}