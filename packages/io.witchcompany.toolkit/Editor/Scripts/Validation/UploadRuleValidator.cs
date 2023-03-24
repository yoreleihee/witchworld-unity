namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class UploadRuleValidator
    {
        public static ValidationReport ValidationCheck()
        {
            return new ValidationReport()
            {
                result = ValidationReport.Result.Success,
                errorMsg = null
            };
        }
    }
}