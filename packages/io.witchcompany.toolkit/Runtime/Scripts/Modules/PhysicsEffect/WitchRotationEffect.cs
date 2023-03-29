using UnityEngine;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    public class WitchRotationEffect : WitchPhysicsEffectBase
    {
        public override string BehaviourName => "물리효과: 회전";

        public override string Description => "물체를 지정한 x,y,z 속도로 회전시킵니다.";

        public override string DocumentURL => throw new System.NotImplementedException();
        
        [Header("회전 속도")] 
        [SerializeField, Range(-100, 100f)] private float speedX = 0f;
        [SerializeField, Range(-100, 100f)] private float speedY = 0.5f;
        [SerializeField, Range(-100, 100f)] private float speedZ = 0;
    }
}