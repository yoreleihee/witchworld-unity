using UnityEngine;
using WitchCompany.Toolkit.Extension;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    [DisallowMultipleComponent]
    public class WitchDoorEffect : WitchPhysicsEffectBase
    {
        public override string BehaviourName => "물리효과: 열리는 문";

        public override string Description => "플레이어가 특정 구역에 들어서면, 문이 열립니다.";

        public override string DocumentURL => "";

        [Header("플레이어를 인지할 Trigger"), SerializeField]
        private Collider playerEnterTrigger;
        [Header("움직일 문 (static이면 안됨)"), SerializeField]
        private Transform targetDoor;
        [Header("문이 움직이는 거리"), SerializeField]
        private Vector3 moveAmount = Vector3.left * 0.4f;
        [Header("문이 움직이는 시간(초)"), SerializeField, Range(0.01f, 10f)]
        private float durationSec = 0.4f;

        public Collider PlayerEnterTrigger => playerEnterTrigger;
        public Transform TargetDoor => targetDoor;
        public Vector3 MoveAmount => moveAmount;
        public float DurationSec => durationSec;
        
#if UNITY_EDITOR
        public override bool ValidationCheck()
        {
            if (playerEnterTrigger == null) return false;
            if (targetDoor == null) return false;
            
            if (!transform.HasChild(playerEnterTrigger)) return false;
            if (!transform.HasChild(targetDoor)) return false;
            
            return true;
        }
#endif
    }
}