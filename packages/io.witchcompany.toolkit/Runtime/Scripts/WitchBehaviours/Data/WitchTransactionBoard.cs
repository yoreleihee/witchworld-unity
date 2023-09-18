using System.Collections.Generic;
using UnityEngine;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchTransactionBoard : WitchBehaviour
    {
        public override string BehaviourName => "";
        public override string Description => "\n" +
                                              "\n\n" +
                                              "*데이터 매니저가 필요합니다.\n";
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchLeaderBoard-4d5aa299eafc4d50b94c48313329d8a4?pvs=4";

        public override int MaximumCount => 8;

        
        [field:Header("순위 종류")]
        [field: SerializeField] public TransRankType RankType { get; private set; }
        
        // [field:Header("조회 기간")] 
        // [field:SerializeField, ReadOnly] public PeriodType PeriodType { get; set; }
        
        
    }
}
