using System;
using UnityEngine;
using UnityEngine.Serialization;
using WitchCompany.Toolkit.Extension;
using WitchCompany.Toolkit.Runtime.Base;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Runtime
{
    public class WitchTeleport : WitchMovementBase
    {
        public override string BehaviourName => "움직임: 텔레포트";

        public override string Description => "블록 내부에서 A 지점에서 B 지점 또는 B 지점에서 A 지점으로 순간 이동할 수 있는 요소입니다.\n" +
                                              "텔레포트의 A, B 지점에는 WitchTeleportCollider 컴포넌트가 필요합니다.\n" +
                                              "텔레포트의 두 지점은 텔레포트의 자식 오브젝트여야 합니다.";
        public override string DocumentURL => "";


        [Header("단방향 (A->B)"), SerializeField]
        private bool isOneWay;
        [Header("A 지점"), SerializeField]
        private WitchTeleportCollider teleportA;
        [Header("B 지점"), SerializeField]
        private WitchTeleportCollider teleportB;
        [Header("텔레포트 활성화 시간"), SerializeField, Range(3f, 100f)]
        private float timer;
        
        
        public bool IsOneWay => isOneWay;
        public WitchTeleportCollider TeleportA => teleportA;
        public WitchTeleportCollider TeleportB => teleportB;
        public float Timer => timer;
        
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (teleportA == null)
                return NullError(nameof(teleportA));
            if (teleportB == null)
                return NullError(nameof(teleportB));

            if (!transform.HasChild(teleportA))
                return ChildError(nameof(teleportA));
            if (!transform.HasChild(teleportB))
                return ChildError(nameof(teleportB));
            
            var distance = Vector3.Distance(teleportA.transform.position, teleportB.transform.position);
            if (distance < Math.Max(teleportA.Collider.radius, teleportB.Collider.radius) + 1)
                return Error($"텔레포트 {gameObject.name}의 두 지점이 겹칩니다. 최소 1m 이상 떨어지게 해 주세요.");

            return base.ValidationCheck();
        }
#endif
    }
}
