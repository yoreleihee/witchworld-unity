using UnityEngine;
using WitchCompany.Toolkit.Extension;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    [DisallowMultipleComponent]
    public class WitchDoorEffect : WitchPhysicsEffectBase
    {
        public override string BehaviourName => "물리효과: 열리는 문";
        public override string Description => "플레이어가 특정 구역에 들어서면, 문이 열립니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 16;

        [Header("플레이어를 인지할 Trigger"), SerializeField]
        private SphereCollider playerEnterTrigger;
        [Header("움직일 문 (static이면 안됨)"), SerializeField]
        private Transform targetDoor;
        [Header("문이 움직이는 거리"), SerializeField]
        private Vector3 moveAmount = Vector3.left * 0.4f;
        [Header("문이 움직이는 시간(초)"), SerializeField, Range(0.01f, 10f)]
        private float durationSec = 0.4f;

        public SphereCollider PlayerEnterTrigger => playerEnterTrigger;
        public Transform TargetDoor => targetDoor;
        public Vector3 MoveAmount => moveAmount;
        public float DurationSec => durationSec;
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (playerEnterTrigger == null) return NullError(nameof(playerEnterTrigger));
            if (targetDoor == null) return NullError(nameof(targetDoor));

            if (!transform.HasChild(playerEnterTrigger)) return ChildError(nameof(playerEnterTrigger));
            if (!transform.HasChild(targetDoor, false)) return ChildError(nameof(targetDoor));
            if (playerEnterTrigger.gameObject == targetDoor.gameObject)
                return Error("playerEnterTrigger와 targetDoor는 다른 오브젝트여야 합니다.");

            if (targetDoor.gameObject.isStatic)
                return new ValidationError($"{name}의 움직일 문({targetDoor.name})은 Static일 수 없습니다.", ValidationTag.Script, targetDoor);
        
            return null;
        }
#endif
    }
}