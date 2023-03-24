using UnityEditor.SceneManagement;
using WitchCompany.Toolkit.Editor.Validation;

namespace WitchCompany.Toolkit.Editor.Tool
{
    public static class UploadPipeline
    {
        public static void UploadWithValidation()
        {
            // 파이프라인 진행 전, 씬 저장 
            EditorSceneManager.SaveOpenScenes();
            
            //// TODO: git 최신버전 체크
            
            //// TODO: 검증 로직 작성
            // 최적화 검증
            if(OptimizationValidator.ValidationCheck().result != ValidationReport.Result.Success)
                return;
            // 씬 구조 룰 검증
            if(WitchBehaviourValidator.ValidationCheck().result != ValidationReport.Result.Success)
                return;
            // 업로드 룰 검증
            if(UploadRuleValidator.ValidationCheck().result != ValidationReport.Result.Success)
                return;
            
            //// 
        }
    }
}