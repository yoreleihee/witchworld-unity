using UnityEngine;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Extension;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public abstract class WitchDisplayBase : WitchBehaviour
    {
        [Header("전시물 인덱스"), SerializeField, ReadOnly]
        protected int index;
        public int Index => index;
        
        [field: Header("디스플레이 타입")]
        [field: SerializeField] public DisplayType DisplayType { get; protected set; }
        
        [Header("썸네일이 그려질 랜더러"), SerializeField] 
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
