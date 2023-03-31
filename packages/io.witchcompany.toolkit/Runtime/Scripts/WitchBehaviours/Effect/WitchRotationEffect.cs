using UnityEngine;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    [DisallowMultipleComponent]
    public class WitchRotationEffect : WitchPhysicsEffectBase
    {
        public override string BehaviourName => "물리효과: 회전";
        public override string Description => "오브젝트에 회전하는 효과를 줍니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 64;
        
        [Header("회전 속도")] 
        [Range(-30f, 30f), SerializeField] private float speedX = 0f;
        [Range(-30f, 30f), SerializeField] private float speedY = 0.5f;
        [Range(-30f, 30f), SerializeField] private float speedZ = 0;

        public Vector3 Speed => new(speedX, speedY, speedZ);
    }
}