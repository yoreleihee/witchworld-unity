using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WitchCompany.Toolkit.Module;

namespace WitchCompany.Toolkit
{
    public class WitchBegging : WitchBehaviour
    {
        public override string BehaviourName => "전시: 구걸함";
        public override string Description => "wit를 구걸할 수 있는 오브젝트입니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 20;
    }
}