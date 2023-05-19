using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    [CreateAssetMenu(fileName = "DataComparator-", menuName = "WitchToolkit/DataComparator")]
    public class WitchDataComparatorSO : ScriptableObject
    {
        [Header("키")]
        public string key = "key";

        [Header("비교할 값")]
        public ComparisonOperator valueOperator = ComparisonOperator.EqualTo;
        public int value = 0;
    }
}
