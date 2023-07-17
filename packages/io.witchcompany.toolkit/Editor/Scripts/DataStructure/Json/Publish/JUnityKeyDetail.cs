namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [System.Serializable]
    public class JUnityKeyDetail
    {
        public string unityKeyType;
        public int count;

        public JUnityKeyDetail(string type)
        {
            unityKeyType = type;
            count = 0;
        }
    }
}