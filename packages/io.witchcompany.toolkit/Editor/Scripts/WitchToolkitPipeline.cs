using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.PackageManager;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.Validation;
using LogLevel = WitchCompany.Toolkit.Editor.Configs.Enum.LogLevel;

namespace WitchCompany.Toolkit.Editor.Tool
{
    public static class WitchToolkitPipeline
    {
        public static JBuildReport PublishWithValidation(BlockPublishOption option)
        {
            var validationReport = new ValidationReport();
            var buildReport = new JBuildReport();

            if (option == null || option.TargetScene == null)
            {
                Debug.LogError("잘못된 option 입니다.");
                buildReport.result = JBuildReport.Result.Failed;
                return buildReport;
            }
            
            try
            {
                // 파이프라인 진행 전, 씬 저장 
                EditorSceneManager.SaveOpenScenes();
                
                // 빌드할 대상 씬으로 전환
                var scnPath = AssetTool.GetAssetPath(option.TargetScene);
                EditorSceneManager.OpenScene(scnPath, OpenSceneMode.Single);
                
                // TODO: Witch Toolkit 최신버전 체크

                // TODO: 검증 로직 작성
                //최적화 검증
                if (validationReport.Append(OptimizationValidator.ValidationCheck()).result != ValidationReport.Result.Success)
                    throw new Exception("최적화 유효성 검사 실패");
                Log("최적화 유효성 검사 성공!");
                
                // 씬 구조 룰 검증
                if (validationReport.Append(WitchRuleValidator.ValidationCheck(option)).result != ValidationReport.Result.Success)
                    throw new Exception("씬 구조 유효성 검사 실패");
                Log("씬 구조 유효성 검사 성공!");

                //// 에셋번들 빌드
                // 번들 전부 지우기
                AssetBundleTool.ClearAllBundles();
                // 번들 할당
                var scenePath = AssetDatabase.GetAssetPath(option.TargetScene);
                var bundleName = option.BundleKey;
                //var bundleName = option.Key;
                AssetBundleTool.AssignAssetBundle(scenePath, bundleName);
                // 번들 빌드
                var manifest = AssetBundleTool.BuildAssetBundle(BuildTarget.StandaloneWindows64);
                Log("번들 빌드 성공!");

                // 업로드 룰 검증
                if (validationReport.Append(UploadRuleValidator.ValidationCheck(option, manifest)).result != ValidationReport.Result.Success)
                    throw new Exception("업로드 유효성 검사 실패");
                Log("업로드 유효성 검사 성공!");
                
                // 업로드 진행
                Debug.Log("블록 생성 성공!");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                Debug.LogError(validationReport.ErrorMsg);
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