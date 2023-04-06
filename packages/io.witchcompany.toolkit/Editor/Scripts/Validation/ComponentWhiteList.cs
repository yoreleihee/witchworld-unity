using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WitchCompany.Toolkit.Editor.Tool;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class ComponentWhiteList
    {
        private static string[] componentTypeWhiteList = new string[]
        {
            "Transform",
            "Terrain",
            "ParticleSystem",
            "Collider",
            "Animator",
            "Rigidbody",
            "MeshFilter",
            // 렌더러 그룹
            "ParticleSystemRenderer",
            "ParticleRenderer",
            "SkinnedMeshRenderer",
            "MeshRenderer",
            "LineRenderer",
            "OcclusionPortal",
            "OcclusionArea",
            // 빛 그룹
            "Light",
            "LightProbeGroup",
            "LightProbeProxyVolume",
            "ReflectionProbe",
            "LensFlare",
            "Skybox",
            // UI 그룹
            "Canvas",
            "CanvasGroup",
            "CanvasRenderer",
            "RectTransform",
            "SpriteMask",
            // Text Mesh Pro
            "InlineGraphic",
            "InlineGraphicManager",
            "TMP_Dropdown",
            "TMP_InputField",
            "TMP_ScrollbarEventHandler",
            "TMP_SelectionCaret",
            "TMP_SpriteAnimator",
            "TMP_SubMesh",
            "TMP_SubMeshUI",
            "TMP_Text",
            "TextMeshPro",
            "TextMeshProUGUI",
            "TextContainer",
            "TMP_Dropdown",
            // 툴킷
            "WitchBehaviour",
        };
        
        public static ValidationReport ValidationCheck()
        {
            var report = new ValidationReport();
            
            var allTransforms = GameObject.FindObjectsOfType<Transform>(true);
            // 오브젝트 단위
            foreach (var tr in allTransforms)
            {
                var components = tr.GetComponents<Component>();
                
                // 컴포넌트 단위
                foreach (var c in components)
                {
                    var fullType = c.GetType();
                    // whitelist에서 컴포넌트 존재 여부 탐색
                    while (fullType != null)
                    {
                        var type = fullType.FullName.Split(".")[^1];
                        var check = Array.Exists(componentTypeWhiteList, x => x == type);
                        
                        // white list에 있으면 통과
                        if (check) break;
                        
                        fullType = fullType.BaseType;
                    }

                    if (fullType == null)
                    {
                        var typeName = c.GetType().FullName.Split(".")[^1];
                        var error = new ValidationError($"Object : {c.name}\n{typeName} 컴포넌트는 배치할 수 없습니다.", "Component", c);
                        report.Append(error);
                    }
                }
            }
            return report;
        }
    }
}