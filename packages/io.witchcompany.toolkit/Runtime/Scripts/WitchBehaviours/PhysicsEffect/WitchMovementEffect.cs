using System;
using UnityEngine;
using UnityEngine.Serialization;
using WitchCompany.Core;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    public class WitchMovementEffect : WitchPhysicsEffectBase
    {
        public override string BehaviourName => "물리효과: 이동";
        public override string Description => "오브젝트를 이동시킬 수 있는 요소입니다.";
        public override string DocumentURL => "";


        public enum CurveType
        {
            Constant,
            EaseIn,
            EaseOut
        }

        [Header("이동할 위치"), SerializeField]
        private Transform target;
        [Header("동작"), SerializeField]
        private CurveType moveType;
        [Header("시간"), SerializeField]
        private float moveTime;
        // [Header("진폭"), SerializeField]
        // private float amplitude;

        public Transform TargetPos => target;
        public float MoveTime => moveTime;
        public CurveType Curve => moveType;
        public float Len => Vector3.Magnitude(target.position - transform.position); 

        public AnimationCurve GetCurve(CurveType type)
        {
            switch (type)
            {
                case CurveType.Constant:
                    return AnimationCurve.Linear(0, 0, moveTime, Len);
                case CurveType.EaseIn:
                    return AnimationCurve.EaseInOut(0, 0, moveTime, Len);
                case CurveType.EaseOut:
                    return AnimationCurve.EaseInOut(0, Len, moveTime, 0);
                default:
                    throw new ArgumentException($"정의되지 않은 MoveType : {type}");
            }
        }
    }
}