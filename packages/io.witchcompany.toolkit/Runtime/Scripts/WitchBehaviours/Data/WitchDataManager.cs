using System.Collections.Generic;
using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchDataManager : WitchBehaviourUnique
    {
        public override string BehaviourName => "데이터 통신: 데이터 매니저";
        public override string Description => "데이터를 읽고 쓸 수 있는 요소입니다.\n" +
                                              "데이터를 변경할 수 있는 기능을 제공합니다.\n" +
                                              "한 씬에 하나만 배치할 수 있습니다.\n";
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchDataManager-8f1f537bf7f04a1d8862f6b0e33b7bfc?pvs=4";
        public override int MaximumCount => 1;

        [field: Header("랭킹보드에서 사용할 키값 (최대 1개, 타이머: @timer, 경과시간: @timespan)")]
        [field: SerializeField] public List<KeyGroup> RankingKeys { get; private set; }
        
        [field: Header("데이터 변경 등록")]
        [field: SerializeField] public List<WitchDataChangerSO> DataChangers { get; private set; }
        
        [System.Serializable]
        public class KeyGroup
        {
            public string key;
            public Alignment alignment;
            public RankingDataType dataType;
        }
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (RankingKeys is {Count: > 1})
                return Error("랭킹보드에서 보여줄 수 있는 키값의 개수는 최대 1개입니다.");

            return base.ValidationCheck();
        }
#endif
    }
}