using System;
using UnityEngine;
using UnityEngine.Serialization;
using WitchCompany.Core;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    public class WitchMovementEffect : WitchPhysicsEffectBase
    {
        public override string BehaviourName => "물리효과: 두 지점 사이 이동";
        public override string Description => "오브젝트를 왕복 이동시킬 수 있는 요소입니다.\n" +
                                              "시작 지점과 끝 지점이 필요합니다.";
        public override string DocumentURL => "";


        public enum CurveType
        {
            Constant,
            EaseIn,
            EaseOut,
            EaseInOut
        }
        [Header("시작 지점"), SerializeField]
        private Transform startPos;
        [Header("끝 지점"), SerializeField]
        private Transform endPos;
        [Header("동작"), SerializeField]
        private CurveType type;
        [Header("시간"), SerializeField]
        private int moveTime;

        public Transform StartTr => startPos;
        public Transform EndTr => endPos;
        public int MoveTime => moveTime;
        public CurveType Curve => type;
        public float Distance => Vector3.Magnitude(endPos.position - startPos.position);

#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (!startPos)
                return NullError("startPos");
            if (!endPos)
                return NullError("targetPos");
            
            return base.ValidationCheck();
        }
#endif
    }
}