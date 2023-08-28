using UnityEditor;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [System.Serializable]
    public class BlockPublishOption
    {
        public string Key => targetScene.name;
        public string BundleKey => Key + ".bundle";
        public string ThumbnailKey => Key + ".jpg";
        
        public SceneAsset targetScene;
        public BundleTheme theme;
        public bool isOfficial;
    }
}