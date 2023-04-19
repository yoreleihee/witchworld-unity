using UnityEngine;

namespace WitchCompany.Toolkit.Runtime.Scripts
{
    [CreateAssetMenu(fileName = "GetValuesData", menuName = "Scriptable Object/GetValues Data", order = int.MaxValue)]
    public class GetValuesData : ScriptableObject
    {
        public string[] keys;
        public int page;
        public int limit;
    }
}