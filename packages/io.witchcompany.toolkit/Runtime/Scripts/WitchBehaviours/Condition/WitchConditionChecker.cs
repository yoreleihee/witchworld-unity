using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Runtime.Scripts.Base;

namespace WitchCompany.Toolkit.Runtime.Scripts
{
    [RequireComponent(typeof(WitchCondition))]
    public class WitchConditionChecker : WitchConditionBase
    {
        public override string BehaviourName => "조건: 결과 확인";
        public override string Description => "조건의 결과값에 따라 지정한 이벤트가 실행됩니다.\n" +
                                              "condition의 결과가 참이면 True Event, 거짓이면 False Event가 실행됩니다.";
        public override string DocumentURL => "";

        [Header("조건"), SerializeField]
        private WitchCondition condition;
        [Header("조건이 참일 때 실행되는 이벤트: True"), SerializeField]
        public UnityEvent onTrue;
        [Header("조건이 거짓일 때 실행되는 이벤트: False"), SerializeField]
        public UnityEvent onFalse;
        
        [HideInInspector] public UnityEvent onCheckCondition;
        

        public WitchCondition Condition => condition;
        public void OnTrue() => onTrue.Invoke();
        public void OnFalse() => onFalse.Invoke();
        public void OnCheckCondition() => onCheckCondition.Invoke();

    }
}