using UnityEngine;
using UnityEngine.Events;

namespace WitchCompany.Toolkit.Module
{
    public class WitchDataManager : WitchBehaviourUnique
    {
        public override string BehaviourName => "데이터 통신: 데이터 매니저";
        public override string Description => "데이터를 읽고 쓸 수 있는 요소입니다.\n" +
                                              "데이터를 변경할 수 있는 기능을 제공합니다.\n" +
                                              "한 씬에 하나만 배치할 수 있습니다.\n";
        public override string DocumentURL => "";
        public override int MaximumCount => 1;

        [HideInInspector] public UnityEvent<WitchDataChangerSO> onChangeValue = new();
        
        public void ChangeValue(WitchDataChangerSO data) => onChangeValue.Invoke(data);
    }
}