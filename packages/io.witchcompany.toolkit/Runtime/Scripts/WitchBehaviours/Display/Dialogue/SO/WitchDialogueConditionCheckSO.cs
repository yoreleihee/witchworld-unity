using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue(ConditionCheck) - ", menuName = "WitchToolkit/Dialogue/ConditionCheck")]
    public class WitchDialogueConditionCheckSO : WitchDialogueSOBase
    {
        [field: Header("비교 방식")]
        [field: SerializeField] public LogicalOperator LogicalOperator  { get; private set; }
        [field: Header("비교할 값들")] 
        [field: SerializeField] public List<WitchDataComparatorSO> Comparators  { get; private set; }
        
        
        [field: Header("조건이 참/거짓일 때의 대화(없다면 대화 종료)")]
        [field: SerializeField] public WitchDialogueSOBase NextDialogueWhenTrue { get; private set; }
        [field: SerializeField] public WitchDialogueSOBase NextDialogueWhenFalse { get; private set; }

        [field: Header("조건이 거짓 때의 이벤트")]
        [field: SerializeField] public UnityEvent OnResultTrue { get; private set; }
        [field: SerializeField] public UnityEvent OnResultFalse { get; private set; }
        
#if UNITY_EDITOR
        public ValidationError ValidationCheck()
        {
            if (Comparators is not {Count: > 0})
                return new ValidationError("비교할 값을 설정해주세요!");
            for (var i = 0; i < Comparators.Count; i++)
            {
                var comp = Comparators[i];
                if (comp == null) return  new ValidationError($"{i}번째 비교자가 null입니다.");
            }

            return null;
        }
#endif
    }
}