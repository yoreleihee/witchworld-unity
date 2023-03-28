using UnityEngine.Serialization;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [System.Serializable]
    public class JAuth
    {
         public string accessToken;
        public int accessExpire;
        public string refreshToken;
        public int refreshExpire;
    }
}