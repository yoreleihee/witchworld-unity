using UnityEngine;
using WitchCompany.Toolkit.Extension;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchDisplayFrame : WitchBehaviourUnique
    {
        // Information
        public override string BehaviourName => "전시 액자";
        public override string Description => "유저가 사진,움짤,영상 등을 등록하여 전시할 수 있는 액자입니다.\n" +
                                              "한 씬에 30개만 배치할 수 있습니다.\n" +
                                              "랜더러 및 콜라이더는 자기 자신이거나, 자식이어야 합니다.";
        public override string DocumentURL => "";

        [Header("사진,움짤,영상 등이 그려질 랜더러"), SerializeField] 
        private Renderer mediaRenderer;
        [Header("유저 클릭에 반응할 콜라이더"), SerializeField]
        private Collider interactionCollider;

        public Renderer MediaRenderer => mediaRenderer;
        public Collider InteractionCollider => interactionCollider;
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (mediaRenderer == null) return NullError(nameof(mediaRenderer));
            if (interactionCollider == null) return NullError(nameof(mediaRenderer));

            if (!transform.HasChild(mediaRenderer)) return ChildError(nameof(mediaRenderer));
            if (!transform.HasChild(interactionCollider)) return ChildError(nameof(interactionCollider));
            
            return null;
        }
#endif
    }
}