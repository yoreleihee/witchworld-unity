using System.Collections.Generic;
using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.Dialogue
{
    public class WitchDialogue : WitchBehaviourUnique
    {
        public override string BehaviourName => "전시: 대화";
        public override string Description => "NPC와 대화할 수 있는 기능입니다.\n" +
                                              "WitchPointerEvent가 필요합니다.\n";
        public override string DocumentURL => "";
        public override int MaximumCount => 10;

        [field: Header("NPC 이름, 목소리")]
        [field: SerializeField] public string NpcName { get; private set; }
        [field: SerializeField] public NpcVoiceType VoiceType { get; private set; }

        [field: Header("시작 대사")]
        [field: SerializeField] public WitchDialogueSOBase InitialDialogue { get; private set; }
        
        [field: Header("대화 시작 시 카메라 위치")]
        [field: SerializeField] public Transform CamPos { get; private set; }
        
#if UNITY_EDITOR
        public override ValidationReport ValidationCheckReport()
        {
            var report = new ValidationReport();
            
            if (string.IsNullOrEmpty(NpcName))
                return report.Append(NullError(nameof(NpcName)));
            if (InitialDialogue == null)
                return report.Append(NullError(nameof(InitialDialogue)));
            if(CamPos == null)
                return report.Append(NullError(nameof(CamPos)));
            if(GetComponent<WitchPointerEvent>() == null)
                return report.Append(NullError("Collider"));

            var list = new List<WitchDialogueSOBase>();
            InitialDialogue.ValidationCheck(ref report, ref list);
            return report;
        }
#endif
    }
}
