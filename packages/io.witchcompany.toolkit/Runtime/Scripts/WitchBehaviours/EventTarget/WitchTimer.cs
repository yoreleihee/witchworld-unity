using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchTimer : WitchBehaviourUnique
    {
        public override string BehaviourName => "타이머";
        public override string Description => "타이머를 시작, 종료할 수 있는 요소입니다.\n" +
                                              "mm:ss:ms 로 표시됩니다.";
        public override string DocumentURL => "";

        public override int MaximumCount => 1;

        [Header("서버 저장 여부")] 
        [SerializeField] private bool saveResultToServer = true;
        
        [HideInInspector] public UnityEvent timerStartEvent;
        [HideInInspector] public UnityEvent timerEndEvent;

        public bool SaveResultToServer => saveResultToServer;

        public void StartTimer() => timerStartEvent.Invoke();
        public void EndTimer() => timerEndEvent.Invoke();
    }
}