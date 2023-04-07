using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [Serializable]
    public class JBuildReport
    {
        public List<JBuildGroup> buildGroups; 
        public Result result = Result.Failed;
        public DateTime BuildStatedAt = DateTime.MinValue;
        public DateTime BuildEndedAt = DateTime.MinValue;
        
        public enum Result
        {
            Failed = -1,
            Canceled = 0,
            Success = 1,
        }
    }
}