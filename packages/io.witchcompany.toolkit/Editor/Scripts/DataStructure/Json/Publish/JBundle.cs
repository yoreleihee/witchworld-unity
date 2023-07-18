using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Serialization;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [System.Serializable]
    public class JBundle
    {
        public JUnityKeyData blockData;
        public List<JManifest> bundleList;
    }
}