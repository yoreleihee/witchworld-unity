using System;
using UnityEngine;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Extension;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchFreeDisplay : WitchBehaviour
    {
        public override string BehaviourName =>"전시: 누구나 배치 가능한 액자";
        public override string Description =>"유저가 사진을 등록하여 전시할 수 있는 액자입니다.\n" +
                                             "블록 주인 뿐 아니라 방문하는 사람도 사진을 등록할 수 있습니다.";
        public override string DocumentURL =>"";
        public virtual int MaximumCount => 40;

        [Header("전시물 인덱스"), SerializeField, ReadOnly]
        private int index;
        public int Index => index;
        
        [field: Header("디스플레이 타입")]
        [field: SerializeField] public DisplayType DisplayType { get; private set; }
        
        [Header("사진이 그려질 랜더러"), SerializeField] 
        private Renderer mediaRenderer;
        public Renderer MediaRenderer => mediaRenderer;
        
        [Header("유저 클릭에 반응할 콜라이더"), SerializeField]
        private Collider interactionCollider;
        public Collider InteractionCollider => interactionCollider;
        
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

        public void Editor_SetIndex(int id) => index = id;
#endif
    }
}
