using UnityEngine;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Extension;
using WitchCompany.Toolkit.Scripts.WitchBehaviours.Display.Enum;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchDisplayFrame : WitchBehaviourUnique
    {
        // Information
        public override string BehaviourName => "전시: 액자";
        public override string Description => "유저가 사진,움짤,영상 등을 등록하여 전시할 수 있는 액자입니다.\n" +
                                              "프레임 오브젝트의 X : Y = 1 : 1 로 해야 합니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 31;

        [Header("사진,움짤,영상 등이 그려질 랜더러"), SerializeField] 
        private Renderer mediaRenderer;
        [Header("유저 클릭에 반응할 콜라이더"), SerializeField]
        private Collider interactionCollider;
        [Header("전시물 인덱스"), SerializeField, ReadOnly]
        private int index;
        
        //23.07.07 추가 코드
        [field: SerializeField] public bool IsNew { get; private set; }
        [field: SerializeField] public DisplayType DisplayType { get; private set; }
        
        public Renderer MediaRenderer => mediaRenderer;
        public Collider InteractionCollider => interactionCollider;
        public int Index => !IsNew ? index : transform.GetSiblingIndex();

#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (mediaRenderer == null) return NullError(nameof(mediaRenderer));
            if (interactionCollider == null) return NullError(nameof(mediaRenderer));

            if (!transform.HasChild(mediaRenderer)) return ChildError(nameof(mediaRenderer));
            if (!transform.HasChild(interactionCollider)) return ChildError(nameof(interactionCollider));

            //scale 변화를 주는 로직이 있기 때문에 static 체크
            if (gameObject.isStatic) return StaticError(gameObject);
            
            return null;
        }
#endif
    }
}