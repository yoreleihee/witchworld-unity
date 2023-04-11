using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Module.Base;
using WitchCompany.Toolkit.Module.PhysicsEffect;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Runtime
{
    [RequireComponent(typeof(WitchDoor))]
    [RequireComponent(typeof(WitchDoorEffect))]
    [DisallowMultipleComponent]
    public class WitchPrivateDoor : WitchBehaviour
    {
        public override string BehaviourName => "통로: 프라이빗 공간";
        public override string Description => "특정 아이템을 가지고 있을 때 프라이빗 공간으로 연결되는 문입니다.\n" +
                                              "입장 권한을 확인할 아이템을 지정해야 합니다.";
        public override string DocumentURL => "";

        // [Header("팝업 제목"), SerializeField]
        // private string popupTitle;

        // todo : url은 door와 privatedoor 중 하나만?! 
        [Header("입장 권할을 지정할 아이템의 키"), SerializeField]
        private int itemKey;
        [Header("설명 제목"), SerializeField]
        private string popupDescriptionTitle;
        [Header("설명"), SerializeField]
        private string popupDescription;
        // [Header("구경하기 링크"), SerializeField]
        // private string viewUrl;
 
        // public string PopupTitle => popupTitle;
        public int ItemKey => itemKey;
        public string PopupDescriptionTitle => popupDescriptionTitle;
        public string PopupDescription => popupDescription;
        // public string ViewUrl => viewUrl;

#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            // if (string.IsNullOrEmpty(popupTitle))
            //     return Error($"프라이빗 도어의 [팝업 제목]을 설정해주세요.");
            
            if (string.IsNullOrEmpty(popupDescriptionTitle))
                return Error($"프라이빗 도어의 [설명 제목]을 설정해주세요.");
            
            if (string.IsNullOrEmpty(popupDescription))
                return Error($"프라이빗 도어의 [설명]을 설정해주세요.");
            
            // if (string.IsNullOrEmpty(viewUrl))
            //     return Error($"프라이빗 도어의 [구경하기 링크]을 설정해주세요.");
            
            return null;
        }
#endif
    }
}
