namespace WitchCompany.Toolkit.Editor.Validation
{
    [System.Serializable]
    public class ValidationReport
    {
        public Result result;
        public string[] errorMsg;
        
        public enum Result
        {
            Failed = -1,
            Canceled = 0,
            Success = 1,
        }
    }
}