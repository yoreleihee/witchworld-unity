using Newtonsoft.Json;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [System.Serializable]
    public class JBundleData
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string bundleUrl;
        public JManifest manifest;
    }
}