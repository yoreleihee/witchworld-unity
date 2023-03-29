using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchCompany.Toolkit.Editor.Validation
{
    [System.Serializable]
    public class ValidationReport
    {
        public Result result = Result.Success;
        public List<string> errMessages = new();
        
        public enum Result
        {
            Failed = -1,
            Canceled = 0,
            Success = 1,
        }

        /// <summary>
        /// 리포트를 추가한다.
        /// 실패할 경우, 메세지를 추가하고 결과를 Failed로 설정한다.
        /// 자기 자신을 리턴하여 메소드 체인으로 사용할 수 있도록 한다.
        /// </summary>
        public ValidationReport Append(string errMsg)
        {
            if (!string.IsNullOrEmpty(errMsg))
            {
                result = Result.Failed;
                
                errMessages ??= new List<string>();
                errMessages.Add(errMsg);
            }
            
            return this;
        }

        public ValidationReport Append(ValidationReport other)
        {
            if (other.result != Result.Success)
            {
                result = other.result;
                if (other.errMessages is {Count: > 0})
                {
                    errMessages ??= new List<string>();
                    errMessages.AddRange(other.errMessages);
                }
            }

            return this;
        }
        
        public string ErrorMsg {
            get
            {
                var builder = new StringBuilder();
                for (var i = 0; i < errMessages.Count; i++)
                {
                    builder.Append(errMessages[i]);
                    if (i < errMessages.Count - 1)
                        builder.Append("\n");
                }

                return builder.ToString();
            }
        }
    }
}