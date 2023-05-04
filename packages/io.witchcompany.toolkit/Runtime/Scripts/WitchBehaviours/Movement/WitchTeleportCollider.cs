using UnityEngine;
using WitchCompany.Toolkit.Runtime.Base;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Runtime
{
    public class WitchTeleportCollider : WitchMovementBase
    {
        public override string BehaviourName => "이동: 텔레포트 콜라이더";
        public override string Description => "텔레포트에서 플레이어를 감지할 수 있는 요소입니다.\n" +
                                              "오브젝트에 isTrigger가 활성화 된 SphereCollider가 필요합니다.";
        public override string DocumentURL => "";
        
        [Header("오브젝트의 SphereCollider"), SerializeField]
        private SphereCollider collider;
        
        public SphereCollider Collider => collider;


#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (collider == null)
                return NullError(nameof(collider));
            if (!TryGetComponent(out SphereCollider col))
                return NullError("SphereCollider");
            if (col.radius <= 0)
                return Error("텔레포트 입구의 크기는 0보다 커야 합니다.");
            if (!col.isTrigger)
                return TriggerError(col);

            return base.ValidationCheck();
        }
#endif
    }
}