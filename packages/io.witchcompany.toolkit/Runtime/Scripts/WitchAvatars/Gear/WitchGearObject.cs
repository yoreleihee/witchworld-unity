using UnityEngine;

namespace WitchCompany.Toolkit
{
    [DisallowMultipleComponent]
    public class WitchGearObject : MonoBehaviour
    {
        // public override string BehaviourName => "상품 : 기어 아이템";
        // public override string Description => $"기어 아이템의 속성을 정할 수 있습니다.\n아바타의 신장은 gearType이 BodySuit일 때만 적용됩니다.";
        // public override string DocumentURL => "";
        
        [Header("기어 종류")]
        [SerializeField] private GearType gearType;
        [Header("비활성화 할 신체 부위")]
        [SerializeField] private SkinType skinType;
        [Header("아바타의 신장(m)"), Range(0.5f, 2f)]
        [SerializeField] private float height = 1f;
        [Header("텍스처 커스터마이징 가능 여부")]
        [SerializeField] private bool isCustomizableTexture;

        public GearType GearType => gearType;
        public SkinType SkinType => skinType;
        public float Height => height;
        public bool IsCustomizableTexture => isCustomizableTexture;

#if UNITY_EDITOR
        // private void OnValidate()
        // {
        //     var enable = gearType == GearType.BodySuit;
        //     ShowField("height", enable);
        // }
        //
        // private void ShowField(string fieldName, bool enable)
        // {
        //     var so = new SerializedObject(this);
        //     var property = so.FindProperty(fieldName);
        //     property.isExpanded = enable;
        //     so.ApplyModifiedProperties();
        // }
#endif

    }
}
