using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace  WitchCompany.Toolkit.Module.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue(Simple) - ", menuName = "WitchToolkit/Dialogue/Simple")]
    public class WitchDialogueSimpleSO : WitchDialogueSOBase
    {
        [field: Header("다음 대화(없다면 대화 종료)")]
        [field: SerializeField] public WitchDialogueSOBase NextDialogue { get; private set; }
        
#if UNITY_EDITOR
        public override void ValidationCheck(ref ValidationReport report)
        {
            base.ValidationCheck(ref report);
            
            if (NextDialogue != null) 
                NextDialogue.ValidationCheck(ref report);
        }
#endif
    }
}