using UnityEngine;
using WitchCompany.Toolkit.Module.Base;
using WitchCompany.Toolkit.Module.PhysicsEffect;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchDoor : WitchPassageBase
    {
        public override string BehaviourName => "통로: 문";
        public override string Description => "다른 실내 공간으로 연결되는 문입니다.\n" +
                                              "연결될 공간의 URL이 필요합니다.\n" +
                                              "문이 열릴때의 효과가 필요합니다. 효과는 자기 자신이어야 합니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 4;

        [Header("문 효과")]
        [SerializeField] private WitchDoorEffect doorEffect;
        public WitchDoorEffect DoorEffect => doorEffect;

#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (doorEffect == null) return NullError(nameof(doorEffect));
            if (doorEffect.gameObject != gameObject) return Error("doorEffect는 자기 자신이어야 합니다.");

            return base.ValidationCheck();
        }
#endif
    }
}