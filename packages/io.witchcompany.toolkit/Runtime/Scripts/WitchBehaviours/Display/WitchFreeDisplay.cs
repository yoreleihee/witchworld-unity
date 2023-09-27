using System;
using UnityEngine;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Extension;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchFreeDisplay : WitchDisplayBase
    {
        public override string BehaviourName =>"전시: 누구나 배치 가능한 액자";
        public override string Description =>"유저가 사진을 등록하여 전시할 수 있는 액자입니다.\n" +
                                             "블록 주인 뿐 아니라 방문하는 사람도 사진을 등록할 수 있습니다.";
        public override string DocumentURL =>"";
        public virtual int MaximumCount => 40;
    }
}
