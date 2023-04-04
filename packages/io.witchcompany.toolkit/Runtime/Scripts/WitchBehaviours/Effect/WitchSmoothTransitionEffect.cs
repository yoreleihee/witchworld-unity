using log4net.Filter;
using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    public class WitchSmoothTransitionEffect : WitchPhysicsEffectBase
    {
        public override string BehaviourName => "효과: 부드럽게 변하기";

        public override string Description => "이 모듈이 부착된 오브젝트는 서서히 나타나거나 사라집니다.\n" +
                                              "변화가 일어날 시간을 설정할 수 있습니다.";

        public override string DocumentURL => "";

        [Header("변화 시간"), SerializeField]
        private int smoothTime;
    }
}