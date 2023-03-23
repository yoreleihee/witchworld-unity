using System;
using UnityEditor;
using UnityEngine;

namespace WitchCompany.Toolkit.Editor.Configs
{
    //[CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class BlockConfig : ScriptableObject
    {
        [field:SerializeField] public string Name { get; private set; }
        [field:SerializeField] public BlockType Type { get; private set; }
        [field:SerializeField] public SceneAsset TargetScene { get; private set; }
    }
}