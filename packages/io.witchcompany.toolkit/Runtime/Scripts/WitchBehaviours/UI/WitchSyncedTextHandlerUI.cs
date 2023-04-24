using UnityEngine;
using WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.Event.Base;

namespace WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.UI
{
    public class WitchSyncedTextHandlerUI : WitchUIBase
    {
        public override string BehaviourName => "UI: 텍스트 동기화 핸들러";
        public override string Description => "자식 오브젝트가 WitchSyncedTextUI 컴포넌트를 가지고 있다면 type에 맞는 값을 Text Mesh Pro의 text로 지정합니다.";
        public override string DocumentURL => "";

        [Header("랭킹"), SerializeField]
        private WitchRankingUI ranking;
        
    }
}