using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    [CreateAssetMenu(fileName = "DataChanger-", menuName = "WitchToolkit/DataChanger")]
    public class WitchDataChanger : ScriptableObject
    {
        [Header("연산자")]
        public ArithmeticOperator valueOperator;
        
        [Header("키/값")]
        public string key;
        public float value;
    }
}
