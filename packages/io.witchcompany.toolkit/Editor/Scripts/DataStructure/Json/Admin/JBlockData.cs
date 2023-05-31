
using Newtonsoft.Json;

namespace WitchCompany.Toolkit.Editor.DataStructure.Admin
{
    [System.Serializable]
    public class JBlockData
    {
        public int unityKeyId;
        public int blockId;
        public JLanguageString blockName;
        public string ownerNickname;
        public string pathName;
        public JLanguageString blockDescription;
        public string blockTheme;
        public string blockType;
        public string blockLevel;
        public bool isPrivate;
        public string itemCA;
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string thumbnailUrl;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public JUnityKey unityKey;
    }
}