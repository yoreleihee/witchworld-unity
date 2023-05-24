using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchDataManager : WitchBehaviourUnique
    {
        public override string BehaviourName => "데이터 통신: 데이터 매니저";
        public override string Description => "데이터를 읽고 쓸 수 있는 요소입니다.\n" +
                                              "데이터를 변경할 수 있는 기능을 제공합니다.\n" +
                                              "한 씬에 하나만 배치할 수 있습니다.\n";
        public override string DocumentURL => "";
        public override int MaximumCount => 1;

        [Header("랭킹보드에서 사용할 키값 (최대 3개, 타이머: @timer)")]
        [SerializeField] private List<KeyGroup> rankingKeys;

        public List<KeyGroup> RankingKeys => rankingKeys;
        
        [HideInInspector] public UnityEvent<WitchDataChangerSO> onChangeValue = new();
        
        public void ChangeValue(WitchDataChangerSO data) => onChangeValue.Invoke(data);

        [System.Serializable]
        public class KeyGroup
        {
            public string key;
            public Alignment alignment;
        }
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (rankingKeys is {Count: > 3})
                return Error("랭킹보드에서 보여줄 수 있는 키값의 개수는 최대 3개입니다.");

            return base.ValidationCheck();
        }
#endif
    }
}