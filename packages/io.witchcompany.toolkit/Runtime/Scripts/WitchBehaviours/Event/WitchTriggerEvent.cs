using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.Event.Base;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.Event
{
    public class WitchTriggerEvent: WitchEventBase
    {
        public override string BehaviourName => "이벤트: 플레이어 감지";

        public override string Description => "해당 구역에 플레이어가 들어오고 나감을 감지할 수 있습니다.\n" + 
                                              "오브젝트에 isTrigger가 활성화된 Collider가 있어야 합니다.";

        public override string DocumentURL => "";

        
        [Header("플레이어 감지 이벤트: Enter"), SerializeField]
        private UnityEvent triggerEnter;
        
        [Header("플레이어 감지 이벤트: Stay"), SerializeField]
        private UnityEvent triggerStay;
        
        [Header("플레이어 감지 이벤트: Exit"), SerializeField]
        private UnityEvent triggerExit;

        
        public UnityEvent TriggerEnter => triggerEnter;
        public UnityEvent TriggerStay => triggerStay;
        public UnityEvent TriggerExit => triggerExit;


#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (!TryGetComponent(out Collider col))
                return NullError("Collider");
            
            if (!col.isTrigger)
                return Error($"{col.name}의 Collider에 isTrigger가 활성화되어야 합니다.");
            
            return base.ValidationCheck();
        }
#endif
    }
}