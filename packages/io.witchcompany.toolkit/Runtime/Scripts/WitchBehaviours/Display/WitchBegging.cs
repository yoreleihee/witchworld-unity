using TMPro;
using UnityEngine;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit
{
    public class WitchBegging : WitchBehaviour
    {
        public override string BehaviourName => "전시: 구걸함";
        public override string Description => "wit를 구걸할 수 있는 요소입니다.\n" +
                                              "해당 오브젝트에는 콜라이더가 있어야 합니다.\n" +
                                              "두 구걸 오브젝트는 동일한 위치에 있어야 합니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 20;

        [Header("구걸함 Index")]
        [SerializeField, ReadOnly] private int index;
        [Header("구걸 비활성화 상태일 때 나타낼 오브젝트")]
        [SerializeField] private GameObject disableObject;
        [Header("구걸 활성화 상태일 때 나타낼 오브젝트")]
        [SerializeField] private GameObject enableObject;
        [Header("구걸 wit를 표시할 텍스트")]
        [SerializeField] private TMP_Text priceText;

        public int Index => index;
        public GameObject DisableObject => disableObject;
        public GameObject EnableObject => enableObject;
        public TMP_Text PriceText => priceText;
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            // 오브젝트 null 검사
            if (disableObject == null) return NullError(nameof(disableObject));
            if (enableObject == null) return NullError(nameof(enableObject));
            
            // 비활성화 오브젝트 콜라이더 유효성 검사
            if(!disableObject.TryGetComponent<Collider>(out var disableObjCol))
                return new ValidationError($"{disableObject.name}은 collider를 가지고 있어야 합니다.", ValidationTag.TagScript, disableObject);
            if(!disableObjCol.enabled)
                return new ValidationError($"{disableObject.name}의 {disableObjCol.name}은 활성화 되어 있어야 합니다.", ValidationTag.TagScript, disableObject);
            
            // 활성화 오브젝트 콜라이더 유효성 검사
            if(!enableObject.TryGetComponent<Collider>(out var enableObjCol))
                return new ValidationError($"{enableObject.name}은 collider를 가지고 있어야 합니다.", ValidationTag.TagScript, enableObject);
            if(!enableObjCol.enabled)
                return new ValidationError($"{enableObject.name}의 {enableObjCol.name}은 활성화 되어 있어야 합니다.", ValidationTag.TagScript, enableObject);

            return base.ValidationCheck();
        }
        
        public void Editor_SetIndex(int idx) => index = idx;
#endif
    }
}