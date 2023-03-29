using System;
using UnityEngine.Serialization;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [Serializable]
    public class JBuildReport
    {
        public string exportPath;
        public int totalSizeByte;
        public Result result;
        public DateTime buildEndedAt;
        
        public enum Result
        {
            Failed = -1,
            Canceled = 0,
            Success = 1,
        }
    }
}