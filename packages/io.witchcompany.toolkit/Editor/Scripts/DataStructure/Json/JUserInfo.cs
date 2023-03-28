using System.Globalization;
using Newtonsoft.Json;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [System.Serializable]
    public class JUserInfo
    {
        [JsonProperty(PropertyName="user")]
        public Profile profile;
        
        [System.Serializable]
        public class Profile
        {
            public string nickname;
            public int role;
            [JsonProperty(PropertyName="default_img")]
            public string defaultImgURL;
        }
    }
}