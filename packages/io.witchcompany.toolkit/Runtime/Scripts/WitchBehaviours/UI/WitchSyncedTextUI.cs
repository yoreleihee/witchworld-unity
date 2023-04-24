using UnityEngine;

namespace WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.Event.Base
{
    public class WitchSyncedTextUI : WitchUIBase
    {

        public override string BehaviourName => "UI: 텍스트 동기화";
        public override string Description => "key로 저장된 값을 가져올 수 있습니다. WitchRankingUI에서 추가한 키 중에 있어야 합니다." +
                                              "같은 오브젝트에 TextMeshPro가 있어야 합니다.";
        public override string DocumentURL => "";
        
        
        public enum StandardType
        {
            Ranking,
            Nickname,
            UpdatedAt
        }

        [Header("Key")]
        // [SerializeField] public bool isCustom;
        // [SerializeField] private StandardType standardType;
        [SerializeField] private string key;

        public string Key => key;

    }
}