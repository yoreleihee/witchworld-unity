using System;
using UnityEngine.Serialization;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [Serializable]
    public class JBuildReport
    {
        public string exportPath;
        public long totalSizeByte = 0;
        public Result result = Result.Failed;
        public DateTime BuildEndedAt = DateTime.MinValue;
        
        public enum Result
        {
            Failed = -1,
            Canceled = 0,
            Success = 1,
        }
    }
}