using UnityEngine;
using UnityEngine.Serialization;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    [DisallowMultipleComponent]
    public class WitchOrbitEffect : WitchPhysicsEffectBase
    {
        public override string BehaviourName => "물리효과: 공전";
        public override string Description => "오브젝트에 외부 축을 기준으로 공전하는 효과를 줍니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 64;

        // 오브젝트의 회전 축을 외부 오브젝트의 Transform 컴포넌트 형식으로 저장
        [Header("회전 축"), Tooltip("회전 축이 될 오브젝트"), SerializeField]
        private Transform orbitPivot;
        // 오브젝트의 회전 속도를 1.0~360.0 사이 임의의 실수 입력 받아서 저장 (초당 회전 각도)
        [Header("회전 속도"), Tooltip("초당 회전 각도"), Range(1f, 360f), SerializeField]
        private float speed = 10f; // 회전 각도의 기본 값은 10.0으로 초기화
        // 오브젝트의 회전 방향을 Vector3 형식으로 저장
        [Header("회전 방향"), SerializeField]
        private Vector3 direction = Vector3.forward;

        // 입력받은 값을 Logic으로 넘겨주기 위해 public 값으로 저장
        public Transform OrbitPivot => orbitPivot;
        public float Speed => speed;
        public Vector3 Direction => direction;

#if UNITY_EDITOR // 유니티 에디터에서
        private void OnValidate()
        {
            if(Application.isPlaying) return; // 애플리케이션 실행중이 아니면

            direction.Normalize(); // 회전 방향 값을 실시간으로 정규화
            // Logic의 RotateAround() 함수에 사용되는 Vector3 angle값은 position값보다 Rotation값에 가깝기 때문에 정규화된 값 사용
        }
#endif
    }
}