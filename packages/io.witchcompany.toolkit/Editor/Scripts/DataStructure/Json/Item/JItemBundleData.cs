namespace WitchCompany.Toolkit.Editor.DataStructure.Item
{
    [System.Serializable]
    public class JItemBundleData
    {
        public JItemData itemData;
        public JBundleInfo standalone;
        public JBundleInfo webgl;
        public JBundleInfo webglMobile;
        public JBundleInfo android;
        public JBundleInfo ios;
        public JBundleInfo vr;
    }
}