namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [System.Serializable]
    public class JResponse<T>
    {
        public string message;
        public T payload;
        public int statusCode;
        public bool success;
    }
}