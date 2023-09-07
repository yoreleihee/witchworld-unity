using UnityEngine;
using UnityEngine.Events;

namespace WitchCompany.Toolkit.Module
{
    public class WitchDelayedEvent : WitchEventBase
    {
        public override string BehaviourName => "이벤트: 지연 이벤트";
        public override string Description => "원하는 시간만큼 기다렸다가 이벤트를 발생시키는 요소입니다.";
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchDelayedEvent-e3501d0c8d18422f94a84ece9ec0c134?pvs=4";

        [field: Header("지연 시간")]
        [field: SerializeField, Range(1f, 60f)] private float delayTime = 1f;

        [field: Header("지연시간 중 이벤트 재시작 가능 여부")]
        [field: SerializeField,
                Tooltip("해당 옵션을 활성화하면 지연시간 중 이벤트 다시 실행 시 타이머를 초기화 합니다.")]
        private bool canRestartTimer = false;

        [field: Header("이벤트"),
                Tooltip("지연시간이 지난 후 실행되는 이벤트입니다.")]
        [field: SerializeField] private UnityEvent delayEvent;

        public float DelayTime => delayTime;
        public bool CanRestartTimer => canRestartTimer;
        public UnityEvent DelayEvent => delayEvent;

        public UnityAction ProcessDelayEvent;

        public void OnDelayEvent()
        {
            ProcessDelayEvent.Invoke();
        }
    }
}