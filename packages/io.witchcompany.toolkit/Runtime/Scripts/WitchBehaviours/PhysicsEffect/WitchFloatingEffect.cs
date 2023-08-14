using UnityEngine;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    [DisallowMultipleComponent]
    public class WitchFloatingEffect : WitchPhysicsEffectBase
    {
        public override string BehaviourName => "물리효과: 둥실둥실";
        public override string Description => "오브젝트에 둥실둥실 뜨는 효과를 줍니다.";
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchFloatingEffect-642a02515fda4021a136e222465a69ff?pvs=4";
        public override int MaximumCount => 64;

        [Header("둥실둥실 높이"),Range(0.01f, 200f), SerializeField]
        private float amplitude = 0.5f;
        [Header("둥실둥실 빠르기"),Range(-10f, 10f), SerializeField]
        private float frequency = 1f;
        [Header("둥실둥실 오프셋"),Range(0f, 1f), SerializeField]
        private float offset = 0.5f;

        public float Amplitude => amplitude;
        public float Frequency => frequency;
        public float Offset => offset;
    }
}