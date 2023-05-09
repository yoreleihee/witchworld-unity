using System;
using UnityEngine;
using WitchCompany.Core;
using WitchCompany.Toolkit.Extension;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    public class WitchMovementEffect : WitchPhysicsEffectBase
    {
        public override string BehaviourName => "물리효과: 두 지점 사이 이동";
        public override string Description => "오브젝트를 왕복 이동시킬 수 있는 요소입니다.\n" +
                                              "시작 지점과 끝 지점이 필요합니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 64;
        
        public enum CurveType
        {
            Constant,
            EaseIn,
            EaseOut,
            EaseInOut
        }
        
        [Header("끝 지점(자식 오브젝트)"), SerializeField]
        private Transform endPos;
        [Header("동작"), SerializeField]
        private CurveType type = CurveType.Constant;
        [Header("시간"), SerializeField, Range(0.1f, 300)]
        private float moveTime = 2f;

        public Transform EndTr => endPos;
        public float MoveTime => moveTime;
        public CurveType Curve => type;

#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (endPos == null)
                return NullError("endPos");
            if (!transform.HasChild(endPos, false))
                return ChildError("endPos");
            if (endPos.GetComponents<MonoBehaviour>().Length > 1)
                return Error("끝 지점은 다른 컴포넌트를 가질 수 없습니다.");

            return base.ValidationCheck();
        }

        private void OnDrawGizmos()
        {
            if(endPos == null) return;
            
            var start = transform.position;
            var end = endPos.position;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(start,end);
            Gizmos.DrawSphere(end, 0.2f);
        }

        private void Reset()
        {
            if (endPos == null && transform.childCount == 0)
            {
                endPos = new GameObject("target").transform;
                endPos.parent = transform;
                endPos.localPosition = Vector3.forward;
            }
        }
#endif
    }
}