namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class OptimizationValidator
    {
        /// <summary>
        /// 최적화 관련 유효성 검사 -> 개별 함수 작성 필요
        /// - 씬에 배치된 오브젝트 전체 가져오기
        /// - 씬에 배치된 개별 매쉬 버텍스 개수 검사
        /// - 씬에 배치된 전체 메쉬 버텍스 개수 검사
        /// - 씬에서 사용된 라이트맵 용량 검사
        /// - 씬에서 사용된 텍스쳐 용량 검사
        /// - 씬에서 유니크 머테리얼 개수 검사 
        /// </summary>
        public static ValidationReport ValidationCheck()
        {
            // TODO: 여기를 수정하세요
            return new ValidationReport()
            {
                result = ValidationReport.Result.Success,
                errMessages = null
            };
        }
    }
}