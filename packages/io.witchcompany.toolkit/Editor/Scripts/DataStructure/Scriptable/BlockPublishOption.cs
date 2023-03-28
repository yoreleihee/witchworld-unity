using System;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs.Enum;

namespace WitchCompany.Toolkit.Editor.Configs
{
    [CreateAssetMenu(fileName = "BlockOption", menuName = "WitchToolkit/BlockOption", order = 0)]
    public class BlockPublishOption : ScriptableObject
    {
        [field:SerializeField] public string Key { get; private set; }

        [field:SerializeField] public BlockTheme Theme { get; private set; }
        [field:SerializeField] public SceneAsset TargetScene { get; private set; }
        
    }
}