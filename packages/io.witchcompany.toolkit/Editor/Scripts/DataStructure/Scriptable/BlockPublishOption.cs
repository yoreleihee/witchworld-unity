using UnityEditor;
using UnityEngine;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    public class BlockPublishOption : ScriptableObject
    {
        public string Key => TargetScene.name;
        public string BundleKey => Key + ".bundle";
        public string ThumbnailKey => Key + ".jpg";

        [field:SerializeField] public BlockTheme Theme { get; private set; }
        [field:SerializeField] public SceneAsset TargetScene { get; private set; }
        
    }
}