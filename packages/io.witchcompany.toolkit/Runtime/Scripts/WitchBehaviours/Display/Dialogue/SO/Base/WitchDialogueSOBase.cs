using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.Dialogue
{
    public abstract class WitchDialogueSOBase : ScriptableObject
    {
        [field: Header("이벤트")]
        [field: SerializeField] public UnityEvent OnStart { get; private set; }
        [field: SerializeField] public UnityEvent OnEnd { get; private set; }

        [field: Header("대사 번역 키 값")]
        //[field: SerializeField] public string localizeKey { get; private set; }
        
        [field: Header("대사")]
        [field: TextArea, SerializeField] public string MessageKr { get; private set; }
        [field:FormerlySerializedAs("<Message>k__BackingField")][field: TextArea, SerializeField] 
        public string MessageEn { get; private set; }

        public virtual bool ShouldWait() => true;
        
#if UNITY_EDITOR
        /// <summary>유효성 검사</summary>
        public virtual void ValidationCheck(ref ValidationReport report, ref List<WitchDialogueSOBase> list)
        {
            list.Add(this);

            report ??= new ValidationReport();

            if (string.IsNullOrEmpty(MessageKr) || string.IsNullOrEmpty(MessageEn))
                report.Append(NullError($"{nameof(MessageKr)} 또는 {nameof(MessageEn)}"));
        }
        
        protected ValidationError Error(string msg) => new(msg, ValidationTag.TagDialogue, this);
        protected ValidationError NullError(string scriptName) => Error($"'{name}'의 {scriptName}를 설정해주세요.");
#endif
    }
}