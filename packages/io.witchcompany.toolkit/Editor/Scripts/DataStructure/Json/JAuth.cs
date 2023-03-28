namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [System.Serializable]
    public class JAuth
    {
        public string authToken;
        public int accessExpire;
        public string refreshToken;
        public int refreshExpire;
    }
}