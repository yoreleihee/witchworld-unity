using UnityEngine;
using WitchCompany.Toolkit.Extension;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchChair : WitchBehaviourUnique
    {
        public override string BehaviourName => "의자";
        public override string Description => "의자입니다.";
        public override string DocumentURL => "";

        [Header("플레이어가 앉을 위치")] 
        [SerializeField] private Transform sitTarget;

        public Transform SitTarget => sitTarget;
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (sitTarget == null) return NullError("sitTarget");
            if (!transform.HasChild(sitTarget, true)) return ChildError("sitTarget");
            
            return null;
        }
#endif
    }
}