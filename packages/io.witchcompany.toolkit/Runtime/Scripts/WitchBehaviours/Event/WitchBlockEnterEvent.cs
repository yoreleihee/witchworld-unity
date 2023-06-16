using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Module;

namespace WitchCompany.Toolkit
{
    public class WitchBlockEnterEvent : WitchBehaviourUnique
    {
        public override string BehaviourName => "블록 입장시 이벤트 발생";
        public override string Description => "블록 입장 성공 시 이벤트를 발생시키는 요소입니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 1;

        // [field: Header("지급할 위트")]
        // [field: SerializeField] public int wit { get; private set; }

        [field: Header("이펙트 위치")]
        [field: SerializeField] public Transform effectPos { get; private set; }

        [HideInInspector] public UnityEvent witReward = new();

        public void WitReward() => witReward.Invoke();
    }
}
