using System;

namespace WitchCompany.Toolkit.Module
{
    [Obsolete("사용 금지!! 미완성 에셋입니다.", true)]
    public class WitchDoor : WitchBehaviourUnique
    {
        public override string BehaviourName => "통로: 문";

        public override string Description => "다른 실내 공간으로 연결되는 문입니다.";

        public override string DocumentURL => "";
    }
}