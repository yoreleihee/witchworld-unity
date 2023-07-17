using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Serialization;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [System.Serializable]
    public class JBundle
    {
        public JUnityKeyData blockData;
        [JsonProperty("bundleList")]
        public List<JManifest> bundleInfoList;
        // public JBundleData standalone;
        // public JBundleData webgl;
        // public JBundleData webglMobile;
        // public JBundleData android;
        // public JBundleData ios;
        // public JBundleData vr;
    }
}