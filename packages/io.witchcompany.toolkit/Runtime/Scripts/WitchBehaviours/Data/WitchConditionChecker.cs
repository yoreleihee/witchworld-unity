using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    [DisallowMultipleComponent]
    public class WitchConditionChecker : WitchDataBase
    {
        public override string BehaviourName => "데이터 통신: 오브젝트 활성화";

        public override string Description => "Key에 해당하는 값을 읽어와\n" +
                                              "조건을 만족할 경우 이벤트를 발생 시키는 기능입니다.\n" +
                                              "And로 연결할 경우 모든 조건을 만족하면 True입니다.\n" +
                                              "Or로 연결할 경우 하나라도 조건을 만족하면 True입니다.\n\n" +
                                              "*데이터 매니저가 필요합니다.";

        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchConditionChecker-f0a8cb5606d14a59b9e23840185c24b4?pvs=4";
        
        [Header("비교 방식")]
        [SerializeField] private LogicalOperator logicalOperator;
        [Header("비교할 값들")] 
        [SerializeField] private List<WitchDataComparatorSO> comparators ;
        
        [Header("조건을 만족할 경우의 이벤트")]
        public UnityEvent onResultTrue = new ();
        [Header("조건을 만족하지 않을 경우의 이벤트")]
        public UnityEvent onResultFalse = new ();

        public List<WitchDataComparatorSO> Comparators => comparators;
        public LogicalOperator Logical => logicalOperator;
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (comparators is not {Count: > 0})
                return Error("비교할 값을 설정해주세요!");
            for (var i = 0; i < comparators.Count; i++)
            {
                var comp = comparators[i];
                if (comp == null) return Error($"{i}번째 비교자가 null입니다.");
            }

            return base.ValidationCheck();
        }
#endif
    }
}
