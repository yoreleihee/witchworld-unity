using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.Dialogue
{
    public abstract class WitchDialogueSOBase : ScriptableObject
    {
        [field: Header("이벤트")]
        [field: SerializeField] public UnityEvent OnStart { get; private set; }
        [field: SerializeField] public UnityEvent OnEnd { get; private set; }

        [field: Header("대사")]
        [field: TextArea, SerializeField] public string Message { get; private set; }

        public virtual bool ShouldWait() => true;
        
#if UNITY_EDITOR
        /// <summary>유효성 검사</summary>
        public virtual void ValidationCheck(ref ValidationReport report, ref List<WitchDialogueSOBase> list)
        {
            list.Add(this);

            report ??= new ValidationReport();

            if (string.IsNullOrEmpty(Message))
                report.Append(NullError(nameof(Message)));
        }
        
        protected ValidationError Error(string msg) => new(msg, ValidationTag.TagDialogue, this);
        protected ValidationError NullError(string scriptName) => Error($"'{name}'의 {scriptName}를 설정해주세요.");
#endif
    }
}