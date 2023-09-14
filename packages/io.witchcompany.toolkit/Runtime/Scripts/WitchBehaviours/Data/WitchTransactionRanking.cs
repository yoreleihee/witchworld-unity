using System.Collections.Generic;
using UnityEngine;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchTransactionRanking : WitchBehaviour
    {
        public override string BehaviourName => "";
        public override string Description => "\n" +
                                              "\n\n" +
                                              "*데이터 매니저가 필요합니다.\n";
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchLeaderBoard-4d5aa299eafc4d50b94c48313329d8a4?pvs=4";

        public override int MaximumCount => 8;
    }
}
