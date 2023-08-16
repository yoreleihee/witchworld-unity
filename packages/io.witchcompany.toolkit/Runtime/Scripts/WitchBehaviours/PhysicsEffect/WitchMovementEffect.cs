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
        public override string Description => "오브젝트를 두 지점을 기준으로 이동시킬 수 있는 요소입니다.\n" +
                                              "시작 지점과 끝 지점이 필요합니다.";
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchMovementEffect-2de3243cd4b84c1eb7c4c6713842846c?pvs=4";
        public override int MaximumCount => 64;
        
        public enum CurveType
        {
            Constant,
            EaseIn,
            EaseOut,
            EaseInOut,
            Oneway
        }
        
        [Header("끝 지점(자식 오브젝트)"), SerializeField]
        private Transform endPos;
        [Header("동작"), SerializeField]
        private CurveType type = CurveType.Constant;
        [Header("시간"), SerializeField, Range(0.1f, 300)]
        private float moveTime = 3f;
        [Header("활성화 시 위치 초기화"), Tooltip("오브젝트를 비활성화 상태에서 활성화 시키면 시작 지점에서 활성화 되도록 설정")]
        private bool activeOnReset = true;

        public Transform EndTr => endPos;
        public float MoveTime => moveTime;
        public CurveType Curve => type;
        public bool ActiveOnReset => activeOnReset;

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