namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [System.Serializable]
    public class JLanguageString
    {
        public string en;
        public string kr;

        public JLanguageString(){}

        public JLanguageString(string en, string kr)
        {
            this.en = en;
            this.kr = kr;
        }
        
        public string GetExistText()
        {
            return string.IsNullOrEmpty(en) ? kr : en;
        }
    }
}