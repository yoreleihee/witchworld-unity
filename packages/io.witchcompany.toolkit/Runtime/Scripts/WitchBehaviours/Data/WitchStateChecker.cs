using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Module;

namespace WitchCompany.Toolkit.Runtime.Scripts
{
    public class WitchStateChecker : WitchBehaviour
    {
        public override string BehaviourName { get; }
        public override string Description { get; }
        public override string DocumentURL { get; }

        [Header("플레이어 감지 이벤트: Exit"), SerializeField]
        public UnityEvent<WitchData> onTrue;

        public void Ontrue(WitchData data) => onTrue.Invoke(data);
        // public UnityEvent TriggerEnter => triggerEnter;
    }
}