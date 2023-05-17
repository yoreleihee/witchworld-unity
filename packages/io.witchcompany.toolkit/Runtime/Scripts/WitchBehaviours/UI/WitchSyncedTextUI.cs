using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Attribute;

namespace WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.Event.Base
{
    public class WitchSyncedTextUI : WitchUIBase
    {
        public override string BehaviourName => "UI: 텍스트 동기화";
        public override string Description => "key로 저장된 값을 가져올 수 있습니다. WitchRankingUI에서 추가한 키 중에 있어야 합니다.\n" +
                                              "같은 오브젝트에 TextMeshPro가 있어야 합니다.";
        public override string DocumentURL => "";
        
        [Header("uiType이 record일 때 key값"),SerializeField, Tooltip("record는 어떤 값이 key로 들어갈지 알 수 없음, 직접 설정해줘야 한다.")] 
        private string key;
        [Header("UI Type"), SerializeField] private UIStandardType uiType;
        
        
        public string Key => key;
        public UIStandardType UiType => uiType;
        

        [HideInInspector]public UnityEvent<UIParam> setText = new();

        public void SetText(UIParam param) => setText.Invoke(param);
    }
}