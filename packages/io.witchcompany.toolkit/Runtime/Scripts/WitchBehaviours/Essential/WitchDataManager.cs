using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using WitchCompany.Toolkit.Runtime.Scripts;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchDataManager : WitchBehaviourUnique
    {
        public override string BehaviourName => "필수: 데이터 매니저";
        public override string Description => "데이터를 읽고 쓸 수 있는 요소입니다.\n" +
                                              "데이터를 변경할 수 있는 기본 연산을 제공합니다.\n" +
                                              "블록 매니저와 같은 오브젝트에 배치되어야 합니다.\n" +
                                              "한 씬에 하나만 배치할 수 있습니다.\n";
        public override string DocumentURL => "";

        [HideInInspector] public UnityEvent<GetValueData> getValue = new();
        [HideInInspector] public UnityEvent<GetValuesData> getValues = new();
        [HideInInspector] public UnityEvent<SetValueData> setValue = new();
        
        public void GetValue(GetValueData data) => getValue.Invoke(data);
        public void GetValues(GetValuesData data) => getValues.Invoke(data);
        public void SetValue(SetValueData data) => setValue.Invoke(data);

    }
}