using UnityEngine;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit
{
    public class WitchBegging : WitchBehaviour
    {
        public override string BehaviourName => "전시: 구걸함";
        public override string Description => "wit를 구걸할 수 있는 요소입니다. 해당 오브젝트에는 콜라이더가 있어야 합니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 20;

        [Header("콜라이더")]
        [SerializeField] private Collider collider;

        public Collider Collider => collider;

        public override ValidationError ValidationCheck()
        {
            var isCol = transform.TryGetComponent<Collider>(out var col);
            if(!isCol)
                return new ValidationError($"{gameObject.name}은 collider를 가지고 있어야 합니다.", ValidationTag.TagScript, transform);
            if(col != collider)
                return new ValidationError($"{gameObject.name}의 collider는 오브젝트에 포함한 Collider이여야 합니다.", ValidationTag.TagScript, transform);
            
            return base.ValidationCheck();
            
        }
    }
}