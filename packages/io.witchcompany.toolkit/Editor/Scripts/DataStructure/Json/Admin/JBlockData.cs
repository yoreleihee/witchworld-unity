
using Newtonsoft.Json;

namespace WitchCompany.Toolkit.Editor.DataStructure.Admin
{
    [System.Serializable]
    public class JBlockData
    {
        public int unityKeyId;
        public string pathName;
        public string ownerNickname;
        public JLanguageString blockName;
        public string blockType;
        public string blockTheme;
        public string blockLevel;
        public JLanguageString blockDescription;
        
        public int blockId;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string thumbnailUrl;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public JUnityKey unityKey;
    }
}