using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WitchCompany.Toolkit.Validation
{
    [System.Serializable]
    public class ValidationReport
    {
        public Result result = Result.Success;
        public List<ValidationError> errors = new();

        public enum Result
        {
            Failed = -1,
            Canceled = 0,
            Success = 1,
        }
        
        public ValidationReport Append(string msg, string tag = "", Object context = null)
        {
            if (!string.IsNullOrEmpty(msg)) 
                Append(new ValidationError(msg, tag, context));

            return this;
        }

        /// <summary>
        /// 리포트를 추가한다.
        /// 실패할 경우, 메세지를 추가하고 결과를 Failed로 설정한다.
        /// 자기 자신을 리턴하여 메소드 체인으로 사용할 수 있도록 한다.
        /// </summary>
        public ValidationReport Append(ValidationError error)
        {
            if (error != null)
            {
                result = Result.Failed;
                
                errors ??= new List<ValidationError>();
                errors.Add(error);
            }
            
            return this;
        }

        public ValidationReport Append(ValidationReport other)
        {
            if (other != null && other.result != Result.Success)
            {
                result = other.result;
                if (other.errors is {Count: > 0})
                {
                    errors ??= new List<ValidationError>();
                    errors.AddRange(other.errors);
                }
            }

            return this;
        }
    }
}