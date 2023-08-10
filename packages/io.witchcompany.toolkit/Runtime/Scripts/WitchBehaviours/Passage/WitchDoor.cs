using System;
using UnityEngine;
using WitchCompany.Toolkit.Module.Base;
using WitchCompany.Toolkit.Module.PhysicsEffect;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    [RequireComponent(typeof(WitchDoorEffect))]
    [Obsolete("더이상 사용되지 않는 기능입니다.", true)]
    public class WitchDoor : WitchPassageBase
    {
        public override string BehaviourName => "통로: 문";
        public override string Description => "다른 실내 공간으로 연결되는 문입니다.\n" +
                                              "연결될 공간의 URL이 필요합니다.\n" +
                                              @" 동일한 오브젝트에 '물리효과: 열리는 문'이 필요합니다.";
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchDoor-4bcb203d1cd34e269e1037c6ea0815f4?pvs=4";
        public override int MaximumCount => 4;

#if UNITY_EDITOR
        public override ValidationError ValidationCheck() => Error("더이상 사용되지 않는 기능입니다.");
#endif
    }
}