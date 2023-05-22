using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.Event
{
    public class WitchEventConnector : WitchBehaviour
    {
        public override string BehaviourName => "이벤트: 연결자";
        public override string Description => "씬 위의 이벤트와 ScriptableObject 이벤트를 연결시켜주는 도구입니다.";
        public override string DocumentURL => "";

        [field: Header("타겟")]
        [field: SerializeField] public WitchEventConnectorSO TargetEvent { get; private set; }
        
        [field: Header("이벤트")]
        [field: SerializeField] public UnityEvent OnEvent { get; private set; }
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (TargetEvent == null)
                return NullError("타겟 이벤트가 null입니다.");

            return base.ValidationCheck();
        }
#endif
    }
}