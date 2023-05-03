using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.Event.Base;
using WitchCompany.Toolkit.Validation;


namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    public class WitchTimer : WitchUIBase
    {
        public override string BehaviourName => "효과: 타이머";
        public override string Description => "타이머를 시작, 종료할 수 있는 요소입니다.\n" +
                                              "TextMeshPro - Text (UI)가 있는 오브젝트에 부착해야 합니다.\n"+
                                              "mm:ss:ms 로 표시됩니다.";
        public override string DocumentURL => "";

        [Header("key"), SerializeField]
        private string key;
        
        [HideInInspector]public UnityEvent startTimer;
        [HideInInspector] public UnityEvent endTimer;

        public string Key => key;
        public void StartTimer() => startTimer.Invoke();
        public void EndTimer() => endTimer.Invoke();
    }
}