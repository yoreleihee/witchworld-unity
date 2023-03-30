using UnityEngine;

namespace WitchCompany.Toolkit.Validation
{
    [System.Serializable]
    public class ValidationError
    {
        public string tag;
        public string message;
        public Object context;

        private ValidationError() {}

        public ValidationError(string msg, string tag = "", Object context = null)
        {
            this.tag = tag;
            this.message = msg;
            this.context = context;
        }
    }
}