using System;
using System.Collections.Generic;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class FreeThemeValidator
    {
        private static readonly string[] PaidComponents =
        {
            nameof(WitchBooth)
        };

        public static ValidationReport ValidationCheck()
        {
            var report = new ValidationReport();
            var allTransforms = GameObject.FindObjectsOfType<Transform>(true);
            
            var allComponents = new Dictionary<string, Component>();
            foreach (var tr in allTransforms)
            {
                var trComponents = tr.GetComponents<Component>();

                foreach (var component in trComponents)
                {
                    var fullType = component.GetType();

                    if (fullType != null)
                    {
                        var type = fullType.FullName.Split(".")[^1];
                        allComponents[type] = component;
                    }
                }
            }

            // 무료 번들이라면
            foreach (var component in PaidComponents)
            {
                var isExist = allComponents.ContainsKey(component);

                if (isExist)
                {
                    var error = new ValidationError($"무료 테마에는 {component}가 포함될 수 없습니다.", "Pay Asset", allComponents[component]);

                    report.Append(error);
                }
            }
            return report;
        }
    }
}