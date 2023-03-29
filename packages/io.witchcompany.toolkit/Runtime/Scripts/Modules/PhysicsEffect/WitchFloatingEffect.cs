using UnityEngine;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    public class WitchFloatingEffect : WitchPhysicsEffectBase
    {
        public override string BehaviourName => "물리효과: 둥실둥실";

        public override string Description => "물체에 둥실둥실 효과를 줍니다.";

        public override string DocumentURL => throw new System.NotImplementedException();
        
        [Header("둥실둥실 높이")] [SerializeField] [Range(0.01f, 200f)]
        private float amplitude = 0.5f;
        [Header("둥실둥실 빠르기")] [SerializeField] [Range(-10f, 10f)]
        private float frequency = 1f;
        [Header("둥실둥실 오프셋")] [SerializeField] [Range(0f, 1f)]
        private float offset = 0.5f;
    }
}