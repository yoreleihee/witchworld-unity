using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider))]
    public class WitchPaintBrush : WitchBehaviour
    {
        public override string BehaviourName => "전시: 페인트 브러쉬";

        public override string Description => "페인트 통 등 낙서 브러쉬의 색상을 변경할 수 있는 요소입니다.\n" +
                                              "낙서 벽의 하위 오브젝트로 두어야 합니다.\n" +
                                              "콜라이더가 필요합니다.";

        public override string DocumentURL => "";
        
        [Header("브러쉬 색상")] 
        [SerializeField] private Color brushColor = Color.black;
        [Header("브러쉬 반지름")]
        [SerializeField, Range(0.01f, 0.5f)] private float brushRadius = 0.1f;
        
        public Color BrushColor => brushColor;
        public float BrushRadius => brushRadius;

#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (!TryGetComponent(out Collider _)) return NullError(nameof(Collider));
            
            if (FindObjectOfType<WitchPaintWall>() == null) 
                return Error("'전시: 페인트 벽'이 씬에 있어야 합니다. 생성해주세요.");
            
            return null;
        }
#endif
    }
}