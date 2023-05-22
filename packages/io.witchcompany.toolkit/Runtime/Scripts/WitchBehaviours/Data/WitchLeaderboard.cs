using System.Collections.Generic;
using UnityEngine;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchLeaderboard : WitchBehaviour
    {
        public override string BehaviourName => "데이터 통신: 리더보드";
        public override string Description => "블록의 랭킹을 나타냅니다.\n" +
                                              "블록의 랭킹에 필요한 키 값을 넣어 랭킹을 불러옵니다.\n\n" +
                                              "*데이터 매니저가 필요합니다.\n";
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchRankingBoard-4d5aa299eafc4d50b94c48313329d8a4?pvs=4";

        public override int MaximumCount => 8;

        [Header("불러올 랭킹 키 값(최대 3개)")] 
        [SerializeField] private List<string> rankingKeys;

        [Header("어디의 랭킹을 불러올지")]
        [SerializeField] private Mode mode;
        [ShowIf(nameof(mode), Mode.OtherBlock), SerializeField] private string blockPathName = "chichi";

        public List<string> RankingKeys => rankingKeys;
        public Mode RankingMode => mode;
        public string BlockPathName => blockPathName;

        public enum Mode
        {
            MyBlock, OtherBlock
        }
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (rankingKeys.Count > 3)
                return Error("리더보드에서 보여줄 수 있는 키값의 개수는 최대 3개입니다.");

            return base.ValidationCheck();
        }
#endif
    }
}
