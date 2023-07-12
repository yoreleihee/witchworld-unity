using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Extension;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchChair : WitchBehaviourUnique
    {
        public override string BehaviourName => "의자";
        public override string Description => "의자입니다.";
        public override string DocumentURL => "";

        [field: Header("플레이어가 앉을 위치")] 
        [field: SerializeField] public Transform sitTarget { get; private set; }
        
        [field: Header("콜라이더")]
        [field: SerializeField] public SphereCollider triggerCollider { get; private set; }
        
        [field:Header("플레이어 인식 범위")]
        [field: SerializeField] public float distance { get; private set; }


#if UNITY_EDITOR
        public override ValidationReport ValidationCheckReport()
        {
            var report = new ValidationReport();
            
            if (sitTarget == null) return report.Append(NullError("sitTarget"));
            if (!transform.HasChild(sitTarget, true)) return report.Append(ChildError("sitTarget"));
            
            if (triggerCollider == null) return report.Append(NullError(nameof(triggerCollider)));
            if (triggerCollider.GetType() != typeof(SphereCollider))
                return report.Append(Error($"{triggerCollider.gameObject}의 collider는 SphereCollider이어야 합니다."));
            if (!triggerCollider.isTrigger) return report.Append(TriggerError(triggerCollider));

            if (distance == 0f) return report.Append(Error("distance는 0 이상이어야 합니다."));
            
            if(GetComponent<WitchPointerEvent>() == null)
                return report.Append(NullError(nameof(WitchPointerEvent)));
            
            return null;
        }
#endif
    }
}