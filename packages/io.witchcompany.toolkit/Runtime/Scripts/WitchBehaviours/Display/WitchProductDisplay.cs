using UnityEngine;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.Display
{
    public class WitchProductUpload : WitchBehaviour
    {
        public override string BehaviourName => "전시: 상품";
        public override string Description => "상품 ID를 입력해 상품을 등록할 수 있습니다.\n" +
                                              "오브젝트에 Collider가 있어야 합니다.\n";
        public override string DocumentURL => "";

         [Header("상품 ID"), SerializeField]
         private int productId;
         

#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if(!TryGetComponent(out Collider col)) return NullError("Collider");
            
            return base.ValidationCheck();
        }
#endif
    }   
}