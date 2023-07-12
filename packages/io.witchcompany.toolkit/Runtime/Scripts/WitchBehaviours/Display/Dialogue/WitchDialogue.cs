using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.Dialogue
{
    public class WitchDialogue : WitchBehaviourUnique
    {
        public override string BehaviourName => "전시: 대화";
        public override string Description => "NPC와 대화할 수 있는 기능입니다.\n" +
                                              "WitchPointerEvent가 필요합니다.\n" +
                                              "NPC 이름은 공백포함 12글자를 넘길 수 없습니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 10;

        [field: Header("NPC 이름, 목소리")]
        [field: SerializeField] public string NpcNameKr { get; private set; }
        [field:FormerlySerializedAs("<NpcName>k__BackingField")][field: SerializeField] 
        public string NpcNameEn { get; private set; }
        [field: SerializeField] public NpcVoiceType VoiceType { get; private set; }

        [field: Header("시작 대사")]
        [field: SerializeField] public WitchDialogueSOBase InitialDialogue { get; private set; }
        
        [field: Header("번역 여부")]
        [field: SerializeField] public DialogueLanguage Language { get; private set; }
        
        [field: Header("대화 시작 시 카메라 위치")]
        [field: SerializeField] public Transform CamPos { get; private set; }
        
#if UNITY_EDITOR
        public override ValidationReport ValidationCheckReport()
        {
            var report = new ValidationReport();
            
            if (string.IsNullOrEmpty(NpcNameKr) || string.IsNullOrEmpty(NpcNameEn))
                return report.Append(NullError($"{nameof(NpcNameKr)} 또는 {nameof(NpcNameEn)}"));
            if (InitialDialogue == null)
                return report.Append(NullError(nameof(InitialDialogue)));
            if(CamPos == null)
                return report.Append(NullError(nameof(CamPos)));
            if(GetComponent<WitchPointerEvent>() == null)
                return report.Append(NullError(nameof(WitchPointerEvent)));
            if (NpcNameKr.Length > 12 || NpcNameEn.Length > 12)
                return report.Append(Error("NPC 이름은 공백포함 12글자를 넘길 수 없습니다."));

            var list = new List<WitchDialogueSOBase>();
            InitialDialogue.ValidationCheck(ref report, ref list);
            return report;
        }
#endif
    }
}
