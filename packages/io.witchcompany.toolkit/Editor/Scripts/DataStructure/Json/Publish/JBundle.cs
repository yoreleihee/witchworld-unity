using Newtonsoft.Json;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [System.Serializable]
    public class JBundle
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public JBlock blockData;
        public JBundleData standalone;
        public JBundleData webgl;
        public JBundleData android;
        public JBundleData ios;
    }
}