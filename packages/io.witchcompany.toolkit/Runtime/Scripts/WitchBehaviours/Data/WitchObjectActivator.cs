using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Logic;
using WitchCompany.Toolkit.Runtime.Scripts.Base;
using WitchCompany.Toolkit.Runtime.Scripts.Enum;

namespace WitchCompany.Toolkit.Runtime
{
    public class WitchObjectActivator : WitchDataBase
    {
        public override string BehaviourName => "Key값에 따라 오브젝트 기능 활성화/비활성화";

        public override string Description => "Key에 해당하는 값을 지정할 수 있는 요소입니다.";

        public override string DocumentURL => "";

        [Header("데이터 통신 key 값")]
        public List<string> keys;
        
        [Header("데이터 받을 때 비교 기준 값")]
        public List<string> comparisonValue;
        
        [Header("데이터 비교 연산자\n" +
                "기준 값 [>연산자<] 비교할 값")]
        [SerializeField] private List<ComparisonOperator> comparisonOperator;


        [HideInInspector] public UnityEvent<WitchObjectActivator> activateChecked = new();
        
        [HideInInspector] public bool activated;

        //값 비교
        public bool CompareValue(string value, int index)
        {
            return Conditional(comparisonValue[index], value, comparisonOperator[index]);
        }

        public int KeyIndex(string key)
        {
            for (int i = 0; i < keys.Count; ++i)
            {
                if (key == keys[i])
                    return i;
            }

            return -1;
        }

        //조건 체크
        private bool Conditional(string operand1, string operand2, ComparisonOperator oper)
        {
            var result = false;
            
            // 실수형...
            if (float.TryParse(operand1, out float op1) && float.TryParse(operand2, out float op2))
            {
                switch (oper)
                {
                    case ComparisonOperator.EqualTo:
                        result = op1 == op2;
                        break;
                    case ComparisonOperator.NotEqualTo:
                        result = op1 != op2;
                        break;
                    case ComparisonOperator.GreaterThan:
                        result = op1 > op2;
                        break;
                    case ComparisonOperator.LessThan:
                        result = op1 < op2;
                        break;
                    case ComparisonOperator.GreaterThanOrEqualTo:
                        result = op1 >= op2;
                        break;
                    case ComparisonOperator.LessThanEqualTo:
                        result = op1 <= op2;
                        break;
                    default: break;
                }
            }
            // 문자열...
            else
            {
                switch (oper)
                {
                    case ComparisonOperator.EqualTo:
                        result = operand1 == operand2;
                        break;
                    case ComparisonOperator.NotEqualTo:
                        result = operand1 != operand2;
                        break;
                    default: break;
                }
            }
            
            return result;
            //return true;
        }
    }
}
