using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.Tool;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class ScriptRuleValidator
    {
        
        
        
        
        /// <summary>
        /// 위치월드 스크립트 룰 관련 유효성 검사
        /// - 블록 옵션 검증
        /// - 블록매니저가 최상위 오브젝트인지 검증
        /// - 유니티 스크립트중 블랙리스트 스크립트 제외
        /// - 개별 WitchBehaviour 검증
        /// - missing script 있는지 검사
        /// </summary>
        public static ValidationReport ValidationCheck(BlockPublishOption option)
        {
            var scene = SceneManager.GetActiveScene();
            return new ValidationReport()
                .Append(ValidateBlockOption(option))
                .Append(ValidateHierarchy(scene))
                .Append(ValidateWitchBehaviours(scene))
                .Append(ValidateMissingScripts(scene))
                .Append(ValidationBoothDistance(scene));
        }

        private static string ValidateBlockOption(BlockPublishOption option)
        {
            const string prefix = "잘못된 블록 옵션: ";
            
            // 타겟 씬이 없으면 실패
            if (option.targetScene == null) 
                return prefix + "타겟 씬이 없음";
            
            // 블록 이름 유효성 검사
            if (!Regex.IsMatch(option.Key, AssetBundleConfig.ValidNameRegex)) 
                return prefix + $"블록 이름 규칙 위반({option.Key}) -> 소문자로 시작하는, 소문자/숫자/언더바로 구성된 12자 이내의 문자열만 가능합니다.";

            return null;
        }

        private static string ValidateHierarchy(Scene scene)
        {
            const string prefix = "잘못된 계층 구조: ";

            var rootObjects = scene.GetRootGameObjects();

            if (rootObjects.Length <= 0)
                return prefix + "씬이 비어있습니다. WitchBlockManager 가 필요합니다.";
            if (rootObjects.Length > 1)
                return prefix + "한 씬의 부모 오브젝트는 하나여야 합니다. WitchBlockManager가 부착된 오브젝트를 만들고, 그 하위로 이동시켜주세요.";
            if (!rootObjects[0].TryGetComponent<WitchBlockManager>(out _))
                return prefix + "최상위 오브젝트는 WitchBlockManager 가 부착되어야 합니다.";

            var comps = rootObjects[0].GetComponents<Component>();
            foreach (var comp in comps)
            {
                if(comp.GetType() == typeof(Transform)) continue;
                if(comp.GetType() == typeof(WitchBlockManager)) continue;
                
                return prefix + "최상위 오브젝트에는 오직 WitchBlockManager만 있어야 합니다.";
            }

            return null;
        }
        
        private static ValidationReport ValidateWitchBehaviours(Scene scene)
        {
            // 블록 매니저를 찾고, WitchBehaviours 찾아서 저장
            var manager = scene.GetRootGameObjects()[0].GetComponent<WitchBlockManager>();
            manager.FindWitchBehaviours(ToolkitConfig.UnityVersion, ToolkitConfig.WitchToolkitVersion);
            EditorSceneManager.SaveOpenScenes();
            
            // 리포트 생성
            var report = new ValidationReport();
            
            // 블록매니저 중복 검증
            foreach (var behaviour in manager.Behaviours)
            {
                if (behaviour.GetType() == typeof(WitchBlockManager))
                    return report.Append("BlockManager는 하나만 있을 수 있습니다.", ValidationTag.TagScript, behaviour);
            }

            // 개별 요소 검증
            report.Append(manager.ValidationCheck());
            report.Append(manager.ValidationCheckReport());
            foreach (var behaviour in manager.Behaviours)
            {
                report.Append(behaviour.ValidationCheck());
                report.Append(behaviour.ValidationCheckReport());
            }

            // 최소 포함요소 검증
            // if (!manager.BehaviourCounter.ContainsKey(typeof(WitchSpawnPoint)))
            //     report.Append("최소 하나 이상의 SpawnPoint를 포함해야 합니다.", ValidationTag.TagScript, manager);
            
            // 요소 개수 검증
            foreach (var (obj, count) in manager.BehaviourCounter.Values)
                if (obj.MaximumCount > 0 && obj.MaximumCount < count)
                    report.Append($"[{obj.BehaviourName}]를 너무 많이 배치했습니다. (현재:{count}개, 최대:{obj.MaximumCount}개)", ValidationTag.TagScript, manager);

            return report;
        }


        private static ValidationReport ValidateMissingScripts(Scene scene)
        {
            var report = new ValidationReport();
            
            var rootObject = scene.GetRootGameObjects()[0].transform;
            var children = HierarchyTool.GetAllChildren(rootObject);

            foreach (var tr in children)
            {
                var components = tr.GetComponents<Component>();
                if (components.Any(c => c == null))
                {
                    var error = new ValidationError($"Object : {tr.gameObject.name}\n찾을 수 없는 스크립트가 포함되어 있습니다.", ValidationTag.TagMissingScript, tr);
                    report.Append(error);
                }
            }
            return report;
        }

        private static ValidationReport ValidationBoothDistance(Scene scene)
        {
            var report = new ValidationReport();
            try
            {
                var booths = new List<WitchBooth>();
                
                
                var rootObject = scene.GetRootGameObjects()[0].transform;
                var children = HierarchyTool.GetAllChildren(rootObject);

                //부스를 가진 오브젝트의 transform 가져오기
                foreach (var tr in children)
                {
                    var objectType = tr.GetComponent<WitchBooth>();
                    if(objectType == null) continue;
                    
                    if (objectType.GetType() == typeof(WitchBooth))
                    {
                        booths.Add(tr.GetComponent<WitchBooth>());
                    }
                }

                var boothArea = 5f;
                
                // 부스를 가진 오브젝트끼리 거리 비교
                for (var i = 0; i < booths.Count - 1; i++)
                {
                    for (var j = i + 1; j < booths.Count; j++)
                    {
                        var distance = Vector3.Distance(booths[i].transform.position, booths[j].transform.position);

                        if (distance < boothArea)
                        {
                            var error = new ValidationError($"{booths[i].name}과 {booths[j].name}의 거리가 너무 가깝습니다.","booth",booths[i]);
                            report.Append(error);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            return report;
        }
    }
}