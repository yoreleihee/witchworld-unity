using System;
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
            
            var validationReport = new ValidationReport();
            var buildReport = new JBuildReport
            {
                result = JBuildReport.Result.Failed
            };

            if (option == null || option.targetScene == null)
            {
                Debug.LogError("잘못된 option 입니다.");
                return buildReport;
            }
            
            try
            {
                // 파이프라인 진행 전, 씬 저장 
                EditorSceneManager.SaveOpenScenes();
                
                // 빌드할 대상 씬으로 전환
                var scnPath = AssetTool.GetAssetPath(option.targetScene);
                if(SceneManager.GetActiveScene().path != scnPath)
                    EditorSceneManager.OpenScene(scnPath, OpenSceneMode.Single);
                
                // TODO: Witch Toolkit 최신버전 체크

                // TODO: 검증 로직 작성
                //최적화 검증
                if (validationReport.Append(OptimizationValidator.ValidationCheck()).result != ValidationReport.Result.Success)
                    throw new Exception("최적화 유효성 검사 실패");
                Log("최적화 유효성 검사 성공!");
                
                // 씬 구조 룰 검증
                if (validationReport.Append(ScriptRuleValidator.ValidationCheck(option)).result != ValidationReport.Result.Success)
                    throw new Exception("씬 구조 유효성 검사 실패");
                Log("씬 구조 유효성 검사 성공!");

                //return buildReport;
                
                //// 에셋번들 빌드
                // 번들 전부 지우기
                AssetBundleTool.ClearAllBundles();
                // 번들 할당
                var scenePath = AssetDatabase.GetAssetPath(option.targetScene);
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
                Log("블록 퍼블리쉬 성공!");

                buildReport.result = JBuildReport.Result.Success;
                buildReport.exportPath = Path.Combine(AssetBundleConfig.BundleExportPath, option.BundleKey);
                buildReport.totalSizeByte = AssetTool.GetFileSizeByte(buildReport.exportPath);
                buildReport.BuildEndedAt = DateTime.Now;
            }
            catch (Exception)
            {
                foreach (var err in validationReport.errors)
                {
                    Debug.LogError(err.message, err.context);
                }
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