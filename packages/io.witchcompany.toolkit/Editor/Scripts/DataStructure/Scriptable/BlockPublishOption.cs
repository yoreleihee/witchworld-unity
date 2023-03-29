using System;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs.Enum;

namespace WitchCompany.Toolkit.Editor.Configs
{
    [CreateAssetMenu(fileName = "BlockOption", menuName = "WitchToolkit/BlockOption", order = 0)]
    public class BlockPublishOption : ScriptableObject
    {
        public string Key => TargetScene.name;
        public string BundleKey => Key + ".bundle";
        public string ThumbnailKey => Key + ".jpg";

        [field:SerializeField] public BlockTheme Theme { get; private set; }
        [field:SerializeField] public SceneAsset TargetScene { get; private set; }
        
    }
}