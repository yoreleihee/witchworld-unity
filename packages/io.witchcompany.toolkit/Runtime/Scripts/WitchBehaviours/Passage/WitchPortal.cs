using System;
using UnityEngine;
using WitchCompany.Toolkit.Module.Base;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    [RequireComponent(typeof(SphereCollider))]
    public class WitchPortal : WitchPassageBase
    {
        public override string BehaviourName => "통로: 포탈";
        public override string Description => "다른 실내/실외 공간으로 연결되는 포탈입니다.\n" +
                                              "입구로 사용될 SphereCollider가 필요합니다.(IsTrigger 필요).\n" +
                                              "입구와 출구는 겹치면 안됩니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 4;

        [Header("출구"), SerializeField] private Transform exit;
        public Transform Exit => exit;

#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (exit == null)
                return NullError(nameof(exit));
            if (!TryGetComponent(out SphereCollider col))
                return NullError("SphereCollider");

            if (col.radius <= 0)
                return Error("포탈 입구의 크기는 0보다 커야 합니다.");
            if (!col.isTrigger)
                return Error("포탈 입구는 IsTrigger 체크되어야 합니다.");

            var distance = Vector3.Distance(exit.position, transform.position);
            if (distance < col.radius + 1)
                return Error("포탈 입구와 출구가 겹칩니다. 입구에서 최소 1m 이상 떨어지게 해 주세요.");

            return base.ValidationCheck();
        }
#endif
    }
}