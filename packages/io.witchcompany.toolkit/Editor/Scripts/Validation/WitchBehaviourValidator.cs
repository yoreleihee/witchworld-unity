namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class WitchBehaviourValidator
    {
        public static ValidationReport ValidationCheck()
        {
            return new ValidationReport
            {
                result = ValidationReport.Result.Success,
                errorMsg = null
            };
        }
    }
}