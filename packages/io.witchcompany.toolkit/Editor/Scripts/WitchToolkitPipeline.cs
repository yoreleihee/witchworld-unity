using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.Validation;

namespace WitchCompany.Toolkit.Editor.Tool
{
    public static class WitchToolkitPipeline
    {
        public static void UploadWithValidation(BlockPublishOption option)
        {
            try
            {
                // 파이프라인 진행 전, 씬 저장 
                EditorSceneManager.SaveOpenScenes();

                //// TODO: Witch Toolkit 최신버전 체크

                //// TODO: 검증 로직 작성
                // 최적화 검증
                if (OptimizationValidator.ValidationCheck().result != ValidationReport.Result.Success)
                    return;
                // 씬 구조 룰 검증
                if (WitchRuleValidator.ValidationCheck(option).result != ValidationReport.Result.Success)
                    return;

                //// 에셋번들 빌드
                // 번들 전부 지우기
                AssetBundleTool.ClearAllBundles();
                // 번들 할당
                var scenePath = AssetDatabase.GetAssetPath(option.TargetScene);
                var bundleName = option.NameEn;
                AssetBundleTool.AssignAssetBundle(scenePath, bundleName);
                // 번들 빌드
                var manifest = AssetBundleTool.BuildAssetBundle(BuildTarget.StandaloneWindows64);
                
                // 업로드 룰 검증
                if (UploadRuleValidator.ValidationCheck(option, manifest).result != ValidationReport.Result.Success)
                    return;
                
                Debug.Log("성공!");
            }
            catch (Exception e)
            {
                Debug.LogError("에셋번들 업로드 파이프라인 예외발생!");
                Debug.LogException(e);
            }
        }
    }
}