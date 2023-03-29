using UnityEngine;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    public class WitchRotationEffect : WitchPhysicsEffectBase
    {
        public override string BehaviourName => "물리효과: 회전";

        public override string Description => "물체를 지정한 x,y,z 속도로 회전시킵니다.";

        public override string DocumentURL => throw new System.NotImplementedException();
        
        [Header("회전 속도")] 
        [Range(-100, 100f)] public float speedX = 0f;
        [Range(-100, 100f)] public float speedY = 0.5f;
        [Range(-100, 100f)] public float speedZ = 0;
    }
}