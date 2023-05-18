using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    [CreateAssetMenu(fileName = "DataComparator-", menuName = "WitchToolkit/DataComparator")]
    public class WitchDataComparator : ScriptableObject
    {
        [Header("연산자")]
        public ComparisonOperator valueOperator;
        
        [Header("키/값")]
        public string key;
        public float value;
    }
}
