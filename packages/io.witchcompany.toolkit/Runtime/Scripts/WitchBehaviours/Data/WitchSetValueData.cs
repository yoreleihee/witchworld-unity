using WitchCompany.Toolkit.Runtime.Scripts.Base;
using WitchCompany.Toolkit.Runtime.Scripts.Enum;

namespace WitchCompany.Toolkit.Runtime.Scripts
{
    public class WitchSetValueData : WitchDataBase
    {
        public override string BehaviourName => "데이터: 값 지정하기";
        public override string Description => "key에 해당하는 값을 지정할 수 있는 요소입니다.";
        public override string DocumentURL => "";
        
        public Owner owner;
        public string key;
        public int value;
        public OperatorType operatorType;
    }
}