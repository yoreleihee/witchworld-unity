using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Runtime.Scripts.Base;
using WitchCompany.Toolkit.Runtime.Scripts.Enum;

namespace WitchCompany.Toolkit.Runtime.Scripts
{
    [Serializable]
    public class Condition
    {
        public WitchGetValueData getValueData;
        public ComparisonOperator comparisonOperator;
        public string operand;
    }
    public class WitchCondition : WitchConditionBase
    {
        public override string BehaviourName => "조건: 조건문";
        public override string Description => "조건문을 지정할 수 있는 요소입니다.\n" +
                                              "조건들이 모두 만족할 경우 True, 하나라도 만족하지 않을 경우 False를 반환합니다.";
        public override string DocumentURL => "";

        [Header("조건문 리스트"), SerializeField]
        private List<Condition> conditionList;

        public List<Condition> ConditionList => conditionList;
    }
}