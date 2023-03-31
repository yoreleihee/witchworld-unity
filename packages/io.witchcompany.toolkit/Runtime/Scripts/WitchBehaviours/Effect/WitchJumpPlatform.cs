using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    [DisallowMultipleComponent, RequireComponent(typeof(Collider))]
    public class WitchJumpPlatform : WitchPhysicsEffectBase
    {
        public override string BehaviourName => "물리효과: 점프 플랫폼";

        public override string Description => "플레이어가 해당 플랫폼을 밟으면 점프합니다. isTrigger 체크가 된 콜라이더가 필요합니다.";

        public override string DocumentURL => "";

        public override int MaximumCount => 64;

        [Header("점프 파워"), SerializeField, Range(0.01f,100f)] 
        private float jumpForce;

        public float JumpForce => jumpForce;
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (!TryGetComponent<Collider>(out var col))
                return Error("isTrigger 체크된 콜라이더가 필요합니다.");

            if (!col.isTrigger)
                return new ValidationError($"{name}의 콜라이더는 isTrigger가 체크되어있어야 합니다.", ValidationTag.Script, col);

            return null;
        }
#endif
    }
}