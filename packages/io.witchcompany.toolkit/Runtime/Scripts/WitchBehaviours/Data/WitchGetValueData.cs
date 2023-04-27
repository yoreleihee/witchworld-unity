using WitchCompany.Toolkit.Runtime.Scripts.Base;
using WitchCompany.Toolkit.Runtime.Scripts.Enum;

namespace WitchCompany.Toolkit.Runtime.Scripts
{
    public class WitchGetValueData : WitchDataBase
    {
        public override string BehaviourName => "데이터: 값 가져오기";
        public override string Description => "블록에 저장된 값을 가져오는 요소입니다.";
        public override string DocumentURL => "";
        
        public Owner owner;
        public string key;
    }
}