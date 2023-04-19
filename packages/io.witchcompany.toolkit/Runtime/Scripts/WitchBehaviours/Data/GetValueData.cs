using UnityEngine;

namespace WitchCompany.Toolkit.Runtime.Scripts
{
    [CreateAssetMenu(fileName = "GetValueData", menuName = "Scriptable Object/GetValue Data", order = int.MaxValue)]
    public class GetValueData : ScriptableObject
    {
        public string owner;
        public string key;
    }
}