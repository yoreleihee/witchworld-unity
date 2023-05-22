using UnityEngine;

namespace  WitchCompany.Toolkit.Module.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue(Simple) - ", menuName = "WitchToolkit/Dialogue/Simple")]
    public class WitchDialogueSimpleSO : WitchDialogueSOBase
    {
        [field: Header("다음 대화(없다면 대화 종료)")]
        [field: SerializeField] public WitchDialogueSOBase NextDialogue { get; private set; }
    }
}