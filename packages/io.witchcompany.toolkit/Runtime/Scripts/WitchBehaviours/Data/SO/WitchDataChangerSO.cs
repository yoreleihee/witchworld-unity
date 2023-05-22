using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    [CreateAssetMenu(fileName = "DataChanger - ", menuName = "WitchToolkit/Data/Changer")]
    public class WitchDataChangerSO : ScriptableObject
    {
        [Header("키")]
        public string key = "key";
        
        [Header("연산할 값")]
        public ArithmeticOperator valueOperator = ArithmeticOperator.Assignment;
        public int value = 0;
    }
}
