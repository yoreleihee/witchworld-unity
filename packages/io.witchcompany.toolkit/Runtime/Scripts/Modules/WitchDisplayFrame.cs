using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    public class WitchDisplayFrame : WitchBehaviour
    {
        // Information
        public override string BehaviourName => "전시 액자";
        public override string Description => "유저가 사진,움짤,영상 등을 등록하여 전시할 수 있는 액자입니다.";
        public override string DocumentURL => "";

        [Header("사진,움짤,영상 등이 그려질 랜더러"), SerializeField] 
        private Renderer mediaRenderer;
        [Header("유저 클릭에 반응할 콜라이더"), SerializeField]
        private Collider interactionCollider;
    }
}