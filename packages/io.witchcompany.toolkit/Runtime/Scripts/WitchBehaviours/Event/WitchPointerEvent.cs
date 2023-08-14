using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchPointerEvent : WitchEventBase
    {
        public override string BehaviourName => "이벤트: 마우스 포인터";

        public override string Description => "오브젝트에 마우스 포인터에 따른 이벤트를 부여할 수 있습니다.\n" +
                                              "오브젝트에 Collider가 있어야 합니다.\n";
                                              
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchPointerEvent-7f10e35c7ac44508b461544c825aaef4?pvs=4";
        
        [Header("Outline 활성화"), SerializeField]
        private bool activeOutline;

        [Header("마우스 포인터 이벤트: Enter"), SerializeField]
        public UnityEvent pointerEnter;

        [Header("마우스 포인터 이벤트: Click"), SerializeField]
        public UnityEvent pointerClick;
        
        [Header("마우스 포인터 이벤트: Exit"), SerializeField]
        public UnityEvent pointerExit;

        public bool ActiveOutline => activeOutline;
        
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