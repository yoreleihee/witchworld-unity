using System.Collections.Generic;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [System.Serializable]
    public class JBundle
    {
        public JUnityKeyData blockData;
        public List<JBundleInfo> bundleInfoList;
        public JReview review;
        public JSalesUnityKeyData salesUnityKey;

    }
}