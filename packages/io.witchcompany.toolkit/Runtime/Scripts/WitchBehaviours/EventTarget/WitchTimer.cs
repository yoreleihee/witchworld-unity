using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.Event.Base;
using WitchCompany.Toolkit.Validation;


namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    public class WitchTimer : WitchUIBase
    {
        public override string BehaviourName => "타이머";
        public override string Description => "타이머를 시작, 종료할 수 있는 요소입니다.\n" +
                                              "TextMeshPro - Text (UI)가 있는 오브젝트에 부착해야 합니다.\n"+
                                              "mm:ss:ms 로 표시됩니다.";
        public override string DocumentURL => "";

        public override int MaximumCount => 1;

        [Header("타이머 키값")] 
        [SerializeField] private string key = "timer";
        [Header("서버 저장 여부")] 
        [SerializeField] private bool saveResultToServer = true;
        
        [HideInInspector] public UnityEvent timerStartEvent;
        [HideInInspector] public UnityEvent timerEndEvent;

        public string Key => key;
        public bool SaveResultToServer => saveResultToServer;

        public void StartTimer() => timerStartEvent.Invoke();
        public void EndTimer() => timerEndEvent.Invoke();
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (string.IsNullOrEmpty(key))
                return Error("timer의 key가 비었습니다.");

            return base.ValidationCheck();
        }
#endif
    }
}