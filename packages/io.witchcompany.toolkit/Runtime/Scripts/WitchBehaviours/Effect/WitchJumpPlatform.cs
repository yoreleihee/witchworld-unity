using UnityEngine;
using WitchCompany.Core;
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

        
        [Header("로컬 좌표 사용 여부"), SerializeField]
        private bool useLocalSpace = true;
        [Header("점프 파워"), SerializeField, Range(3f,30f)] 
        private float jumpVelocity = 8f;
        [Header("방향"), SerializeField]
        private Vector3 direction = Vector3.up;

        public Vector3 JumpVector => (useLocalSpace ? transform.TransformDirection(direction) : direction) * jumpVelocity;

#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (!TryGetComponent<Collider>(out var col))
                return NullError("Collider");

            if (col.isTrigger) return TriggerError(col);

            return null;
        }
        
        private void OnDrawGizmos()
        {
            var dir = useLocalSpace ? transform.TransformDirection(direction) : direction;
            var post = transform.position;
            var len = Mathf.Clamp(jumpVelocity/4f, 0.5f, 10f);
            
            CustomGizmo.DrawArrow(post, post + dir * len, Color.red);
        }

        private void OnValidate()
        {
            if(Application.isPlaying) return;
            
            direction.Normalize();
        }
#endif
    }
}