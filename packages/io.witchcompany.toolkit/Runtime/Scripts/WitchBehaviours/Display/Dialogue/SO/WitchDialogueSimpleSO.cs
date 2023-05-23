using System.Collections.Generic;
using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace  WitchCompany.Toolkit.Module.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue(Simple) - ", menuName = "WitchToolkit/Dialogue/Simple")]
    public class WitchDialogueSimpleSO : WitchDialogueSOBase
    {
        [field: Header("다음 대화(없다면 대화 종료)")]
        [field: SerializeField] public WitchDialogueSOBase NextDialogue { get; private set; }

        /// <summary>대기 해야하는지?</summary>
        public override bool ShouldWait() => false;

#if UNITY_EDITOR

        public override void ValidationCheck(ref ValidationReport report, ref List<WitchDialogueSOBase> list)
        {
            if (list.Contains(this)) return;
            base.ValidationCheck(ref report, ref list);
            
            if (NextDialogue != null && NextDialogue != this) 
                NextDialogue.ValidationCheck(ref report, ref list);
        }
#endif
    }
}