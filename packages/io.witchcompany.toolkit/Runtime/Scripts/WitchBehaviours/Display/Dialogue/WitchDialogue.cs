using UnityEngine;
using WitchCompany.Toolkit.Module;

namespace WitchCompany.Toolkit
{
    public class WitchDialogue : WitchBehaviourUnique
    {
        public override string BehaviourName => "전시: 대화";
        public override string Description => "NPC와 대화할 수 있는 기능입니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 10;
        
        
    }
}
