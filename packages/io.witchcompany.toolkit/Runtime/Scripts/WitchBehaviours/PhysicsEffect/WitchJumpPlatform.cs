using UnityEngine;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    [DisallowMultipleComponent, RequireComponent(typeof(Collider))]
    public class WitchJumpPlatform : WitchPhysicsEffectBase
    {
        public override string BehaviourName => "물리효과: 점프 플랫폼";

        public override string Description => "플레이어가 해당 플랫폼을 밟으면 점프합니다. isTrigger 체크가 된 콜라이더가 필요합니다.";

        public override string DocumentURL => "";

        [Header("점프 파워"), SerializeField, Range(0.01f,100f)] 
        private float jumpForce;

        public float JumpForce => jumpForce;
        
#if UNITY_EDITOR
        public override bool ValidationCheck()
        {
            return 
                TryGetComponent<Collider>(out var col) && 
                col.isTrigger;
        }
#endif
    }
}