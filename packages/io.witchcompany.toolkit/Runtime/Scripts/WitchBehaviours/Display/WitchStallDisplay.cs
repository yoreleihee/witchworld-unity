using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    public class WitchStallDisplay : WitchDisplayBase
    {
        public override string BehaviourName =>"전시: 아이템 판매 등록이 가능한 액자";
        public override string Description =>"유저가 아이템을 등록하여 판매등록할 수 있는 액자입니다.\n" +
                                             "블록 주인 뿐 아니라 방문하는 사람도 아이템을 등록할 수 있습니다.";
        public override string DocumentURL =>"";
        public virtual int MaximumCount => 40;
    }
}
