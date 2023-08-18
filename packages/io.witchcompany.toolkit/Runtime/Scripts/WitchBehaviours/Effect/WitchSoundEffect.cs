using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit
{
    public class WitchSoundEffect : WitchBehaviour
    {
        public override string BehaviourName => "효과: SE 재생";

        public override string Description => "SE를 재생하는 효과입니다.\n" +
                                              "Play 이벤트를 바인딩하여 사용합니다.";

        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchSoundEffect-4dfd2e0014c24060a11fa84fcd1c9023?pvs=4";

        public override int MaximumCount => 32;

        [field: Header("재생할 사운드")] 
        [field: SerializeField] public AudioClip TargetClip { get; private set; }

        public UnityAction PlayAction;

        public void PlaySE() => PlayAction.Invoke();
        
        
#if UNITY_EDITOR
        public override ValidationReport ValidationCheckReport()
        {
            var report = new ValidationReport();

            if (TargetClip == null)
                return report.Append(NullError(nameof(TargetClip)));
           
            return base.ValidationCheckReport();
        }
#endif
    }
}