using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    [RequireComponent(typeof(Collider))]
    public class WitchProductDisplay : WitchBehaviour
    {
        public override string BehaviourName => "전시: 상품";
        public override string Description => "상품 ID를 입력해 상품을 등록할 수 있습니다.\n" +
                                              "오브젝트에 Collider가 있어야 합니다.\n";
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchProductDisplay-a6e53ca27be94afca64d769108304ec2?pvs=4";

         [Header("상품 ID"), SerializeField] private int productId;
         [Header("상품 ID (개발서버)"), SerializeField] private int productIdDev;

         public int ProductId => productId;
         public int ProductIdDev => productIdDev;
         
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if(!TryGetComponent(out Collider _)) return NullError("Collider");
            
            return base.ValidationCheck();
        }
#endif
    }   
}