using UnityEngine;
using UnityEngine.Events;

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
        
        [Header("서버에 쓸지?")] 
        public bool saveOnServer = true;

        [HideInInspector] public UnityEvent<WitchDataChangerSO> onEvent = new();
        
        public void Invoke() => onEvent.Invoke(this);
    }
}
