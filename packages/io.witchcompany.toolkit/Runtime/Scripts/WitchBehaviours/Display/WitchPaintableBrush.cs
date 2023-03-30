using UnityEngine;
using WitchCompany.Toolkit.Extension;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    [DisallowMultipleComponent]
    public class WitchPaintableBrush : WitchBehaviour
    {
        public override string BehaviourName => "페인트 브러쉬";

        public override string Description => "낙서 브러쉬의 색상을 변경할 수 있는 요소입니다.\n" +
                                              "낙서 벽의 하위 오브젝트로 두어야 합니다.\n" +
                                              "콜라이더가 필요합니다.";

        public override string DocumentURL => "";

        [Header("변경할 색상")] 
        public Color targetColor;

        [Header("클릭 콜라이더")] 
        public Collider clickCollider;

        public Color TargetTargetColor => targetColor;
        public Collider ClickCollider => clickCollider;
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (clickCollider == null) return NullError(nameof(clickCollider));
            if (!transform.HasChild(clickCollider)) return ChildError(nameof(clickCollider));
            
            return null;
        }
#endif
    }
}