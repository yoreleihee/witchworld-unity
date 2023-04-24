using System;
using WitchCompany.Toolkit.Module;

namespace WitchCompany.Toolkit.Runtime.Scripts
{
    [Serializable]
    public class WitchData : WitchBehaviour
    {
        public string key;
        public override string BehaviourName { get; }
        public override string Description { get; }
        public override string DocumentURL { get; }
    }
}