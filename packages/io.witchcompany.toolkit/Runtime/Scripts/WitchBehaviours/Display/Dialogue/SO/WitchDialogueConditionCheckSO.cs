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
        public override void ValidationCheck(ref ValidationReport report, ref List<WitchDialogueSOBase> list)
        {
            if (list.Contains(this)) return;
            base.ValidationCheck(ref report, ref list);

            if (FindObjectOfType<WitchDataManager>() == null)
            {
                report.Append(NullError(nameof(WitchDataManager)));
                return;
            }

            if (Comparators is not {Count: > 0})
                report.Append(NullError(nameof(Comparators)));
            
            for (var i = 0; i < Comparators.Count; i++)
                if (Comparators[i] == null)
                    report.Append(Error($"'{name}'의 {i}번째 비교자가 null입니다."));

            if (NextDialogueWhenTrue != null && NextDialogueWhenTrue != this)
                NextDialogueWhenTrue.ValidationCheck(ref report, ref list);
            if (NextDialogueWhenFalse != null && NextDialogueWhenFalse != this)
                NextDialogueWhenFalse.ValidationCheck(ref report, ref list);
        }
#endif
    }
}