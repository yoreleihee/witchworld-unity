using UnityEngine;
using UnityEngine.Serialization;
using WitchCompany.Toolkit.Module.Base;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchTeleportCollider : WitchMovementBase
    {
        public override string BehaviourName => "이동: 텔레포트 콜라이더";
        public override string Description => "텔레포트에서 플레이어를 감지할 수 있는 요소입니다.\n" +
                                              "오브젝트에 isTrigger가 활성화 된 SphereCollider가 필요합니다.";
        public override string DocumentURL => "";
        
        
        [Header("오브젝트의 SphereCollider")]
        [FormerlySerializedAs("collider")]
        [SerializeField]
        private SphereCollider sphereCollider;
        
        public SphereCollider SphereCollider => sphereCollider;


#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (sphereCollider == null)
                return NullError(nameof(sphereCollider));
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