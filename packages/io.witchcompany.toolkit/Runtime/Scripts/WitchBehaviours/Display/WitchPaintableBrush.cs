using UnityEngine;
using WitchCompany.Toolkit.Extension;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    [DisallowMultipleComponent]
    public class WitchPaintableBrush : WitchBehaviour
    {
        public override string BehaviourName => "페인트 브러쉬";

        public override string Description => "페인트 통 등 낙서 브러쉬의 색상을 변경할 수 있는 요소입니다.\n" +
                                              "낙서 벽의 하위 오브젝트로 두어야 합니다.\n" +
                                              "콜라이더가 필요합니다.";

        public override string DocumentURL => "";

        [Header("클릭 콜라이더")] 
        public Collider clickCollider;
        
        [Header("브러쉬 색상")] 
        [SerializeField] private Color brushColor = Color.black;
        [Header("브러쉬 반지름")]
        [SerializeField, Range(0.01f, 0.5f)] private float brushRadius = 0.1f;
        
        public Collider ClickCollider => clickCollider;
        public Color BrushColor => brushColor;
        public float BrushRadius => brushRadius;

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