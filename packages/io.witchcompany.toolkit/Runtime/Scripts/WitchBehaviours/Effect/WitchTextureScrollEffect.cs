using UnityEngine;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(MeshRenderer))]
    public class WitchTextureScrollEffect : WitchBehaviour
    {
        public override string BehaviourName => "효과: 텍스쳐 스크롤";

        public override string Description => "텍스쳐를 일정 속도로 움직입니다. MeshRenderer가 필요합니다.";

        public override string DocumentURL => "";

        [Header("X축 속도"), SerializeField, Range(-10, 10)]
        private float speedX;
        [Header("Y축 속도"), SerializeField, Range(-10, 10)]
        private float speedY;

        public Vector2 Speed => new(speedX, speedY);
    }
}