using UnityEngine;
using UnityEngine.Events;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    public class WitchSmoothTransitionEffect : WitchPhysicsEffectBase
    {
        public override string BehaviourName => "효과: 부드럽게 변하기";

        public override string Description => "이 모듈이 부착된 오브젝트는 서서히 나타나거나 사라집니다.\n" +
                                              "변화가 일어날 시간을 설정할 수 있습니다.\n";

        public override string DocumentURL => "";

        [Header("변화 시간"), SerializeField, Range(0.01f, 2f)]
        private float smoothTime = 0.1f;
        [HideInInspector]
        public UnityEvent show = new();
        [HideInInspector]
        public UnityEvent hide = new();
        
        public float SmoothTime => smoothTime;

        public void Show() => show.Invoke();
        public void Hide() => hide.Invoke();
    }
}