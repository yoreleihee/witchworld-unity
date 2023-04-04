using UnityEngine.Serialization;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [System.Serializable]
    public class JAuth
    {
        public string accessToken;
        public long accessExpire;
        public string refreshToken;
        public long refreshExpire;
        public JRegisterBlockData adminBlockData;
    }
}