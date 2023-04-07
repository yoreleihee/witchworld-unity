using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.Validation;
using WitchCompany.Toolkit.Validation;
using LogLevel = WitchCompany.Toolkit.Editor.DataStructure.LogLevel;

namespace WitchCompany.Toolkit.Editor.Tool
{
    public static class WitchToolkitPipeline
    {
        public static JBuildReport PublishWithValidation(BlockPublishOption option)
        {
            Log("블록 퍼블리쉬 시작!");
            
            // 리포트 생성
            var validationReport = new ValidationReport();
            var buildReport = new JBuildReport
            {
                result = JBuildReport.Result.Failed,
                BuildStatedAt = DateTime.Now
            };

            try
            {
                // 옵션 체크
                if (option == null || option.targetScene == null)
                {
                    Debug.LogError("잘못된 option 입니다.");
                    return buildReport;
                }
                
                // 파이프라인 진행 전, 씬 저장 
                EditorSceneManager.SaveOpenScenes();
                
                // 빌드할 대상 씬으로 전환
                var scnPath = AssetTool.GetAssetPath(option.targetScene);
                if(SceneManager.GetActiveScene().path != scnPath)
                    EditorSceneManager.OpenScene(scnPath, OpenSceneMode.Single);
                
                // TODO: Witch Toolkit 최신버전 체크

                //최적화 검증
                if (validationReport.Append(OptimizationValidator.ValidationCheck()).result != ValidationReport.Result.Success)
                    throw new Exception("최적화 유효성 검사 실패");
                Log("최적화 유효성 검사 성공!");
                
                // 씬 구조 룰 검증
                if (validationReport.Append(ScriptRuleValidator.ValidationCheck(option)).result != ValidationReport.Result.Success)
                    throw new Exception("씬 구조 유효성 검사 실패");
                Log("씬 구조 유효성 검사 성공!");
                
                // 오브젝트 검증
                if(validationReport.Append(ObjectValidator.ValidationCheck()).result != ValidationReport.Result.Success)
                    throw new Exception("오브젝트 유효성 검사 실패");
                Log("오브젝트 유효성 검사 성공!");
                
                //화이트리스트 검증
                if(validationReport.Append(WhiteListValidator.ValidationCheck()).result != ValidationReport.Result.Success)
                    throw new Exception("화이트리스트 검사 실패");
                Log("화이트리스트 검사 성공!");

                // Static 풀어주기
                if(validationReport.Append(StaticRevertTool.SaveAndClearFlags()).result != ValidationReport.Result.Success)
                    throw new Exception("static 캐싱 실패");
                EditorSceneManager.SaveOpenScenes();

                //// 에셋번들 빌드
                // 번들 전부 지우기
                AssetBundleTool.ClearAllBundles();
                // 번들 할당
                var scenePath = AssetDatabase.GetAssetPath(option.targetScene);
                var bundleName = option.BundleKey;
                AssetBundleTool.AssignAssetBundle(scenePath, bundleName);
                // 번들 빌드
                var bundles = AssetBundleTool.BuildAssetBundle();
                Log("번들 빌드 성공!: " + bundles.Count);
                
                // Static 되돌려주기
                StaticRevertTool.RevertFlags();
                EditorSceneManager.SaveOpenScenes();

                // 업로드 룰 검증
                foreach (var (target, bundle) in bundles)
                {
                    if (validationReport.Append(UploadRuleValidator.ValidationCheck(option, target, bundle)).result != ValidationReport.Result.Success)
                        throw new Exception("업로드 유효성 검사 실패");
                }

                Log("업로드 유효성 검사 성공!");
                
                // 빌드 리포트 작성
                buildReport.result = JBuildReport.Result.Success;
                buildReport.buildGroups = new List<JBuildGroup>();
                foreach (var (target, bundle) in bundles)
                {
                    var group = new JBuildGroup();
                    group.target = target;
                    group.exportPath = Path.Combine(AssetBundleConfig.BundleExportPath, target, option.BundleKey);
                    group.totalSizeByte = AssetTool.GetFileSizeByte(group.exportPath);
                }
                buildReport.BuildEndedAt = DateTime.Now;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                foreach (var err in validationReport.errors) 
                    Debug.LogError(err.message, err.context);
            }

            return buildReport;
        }

        private static void Log(string msg)
        {
            if (ToolkitConfig.CurrLogLevel.HasFlag(LogLevel.Pipeline))
                Debug.Log("[Pipeline] " + msg);
        }
    }
}