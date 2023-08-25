using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchTriggerEvent: WitchEventBase
    {
        public override string BehaviourName => "이벤트: 플레이어 감지";

        public override string Description => "해당 구역에 플레이어가 들어오고 나감을 감지할 수 있습니다.\n" + 
                                              "오브젝트에 isTrigger가 활성화된 Collider가 있어야 합니다.";

        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchTriggerEvent-1e09ee8ed0994ff2a75e2fb718ce47c4?pvs=4";

        
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
        public override ValidationReport ValidationCheckReport()
        {
            var report = new ValidationReport();
            
            if (!TryGetComponent(out Collider col))
                report.Append(NullError("Collider"));
            
            if (!col.isTrigger)
                report.Append(Error($"{col.name}의 Collider에 isTrigger가 활성화되어야 합니다."));
            
            report.Append(EventHandlerCheck(triggerEnter));
            report.Append(EventHandlerCheck(triggerStay));
            report.Append(EventHandlerCheck(triggerExit));

            return report;
        }
#endif
    }
}