using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.Event.Base;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.Event
{
    public class WitchPointerEvent : WitchEventBase
    {
        public override string BehaviourName => "이벤트: 마우스 포인터";

        public override string Description => "오브젝트에 마우스 포인터에 따른 이벤트를 부여할 수 있습니다.\n" +
                                              "오브젝트에 Collider가 있어야 합니다.\n";
                                              
        public override string DocumentURL => "";
        
        [Header("Outline 활성화"), SerializeField]
        private bool outline;

        [Header("마우스 포인터"), SerializeField]
        private UnityEvent pointerEvent;
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if(!TryGetComponent(out Collider col))
                return NullError("Collider"); 

            return base.ValidationCheck();
        }
#endif
    }
}