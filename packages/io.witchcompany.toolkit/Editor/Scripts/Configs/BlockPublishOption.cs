using System;
using UnityEditor;
using UnityEngine;

namespace WitchCompany.Toolkit.Editor.Configs
{
    [CreateAssetMenu(fileName = "BlockOption", menuName = "WitchToolkit/BlockOption", order = 0)]
    public class BlockPublishOption : ScriptableObject
    {
        [field:SerializeField] public string NameEn { get; private set; }
        [field:SerializeField] public string NameKr { get; private set; }
        [field:SerializeField] public BlockType Type { get; private set; }
        [field:SerializeField] public SceneAsset TargetScene { get; private set; }
        
        public enum BlockType {Indoor = 0, Outdoor}
    }
}