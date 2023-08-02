using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    public class JRankingKey
    {
        public string level;
        public string key;
        [JsonProperty(PropertyName="type")]
        public string sortType;
        public string dataType;
    }
}