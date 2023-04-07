using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Module.Base;
using WitchCompany.Toolkit.Module.PhysicsEffect;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    [RequireComponent(typeof(WitchDoorEffect))]
    public class WitchDoor : WitchPassageBase
    {
        public override string BehaviourName => "통로: 문";
        public override string Description => "다른 실내 공간으로 연결되는 문입니다.\n" +
                                              "연결될 공간의 URL이 필요합니다.\n" +
                                              @" 동일한 오브젝트에 '물리효과: 열리는 문'이 필요합니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 4;

        [Header("나가는 방향(읽기 전용)")]
        [SerializeField, ReadOnly] private Vector3 outerDirection;
        public Vector3 OuterDirection => outerDirection;
        

#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if(!TryGetComponent<WitchDoorEffect>(out _))
                return NullError(nameof(WitchDoorEffect));
            
            return base.ValidationCheck();
        }
#endif
    }
}