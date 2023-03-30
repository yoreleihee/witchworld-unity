using System;
using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    [Obsolete("사용 금지!! 미완성 에셋입니다.", true)]
    public class WitchPortal : WitchBehaviourUnique
    {
        public override string BehaviourName => "통로: 포탈";
        public override string Description => "다른 실내/실외 공간으로 연결되는 포탈입니다.";
        public override string DocumentURL => "";
    }
}