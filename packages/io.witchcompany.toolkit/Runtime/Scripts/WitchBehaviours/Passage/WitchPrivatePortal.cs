using UnityEngine;
using WitchCompany.Toolkit.Module.Base;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    [RequireComponent(typeof(SphereCollider))]
    public class WitchPrivatePortal : WitchPassageBase
    {
        public override string BehaviourName => "통로 : 프라이빗 포탈";
        public override string Description => "특정 아이템을 가지고 있을 때 프라이빗 블록으로 연결되는 포탈입니다.\n" +
                                              "입구로 사용할 isTrigger가 활성화된 SphereCollider가 필요합니다.\n" +
                                              "입구와 출구는 겹치면 안됩니다.";
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchPrivatePortal-211ba720ca834e86ad4de92631b64c70?pvs=4";
        
        public override int MaximumCount => 10;
        
        [Header("출구"), SerializeField]
        private Transform exit;
        
        public Transform Exit => exit;

#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (exit == null)
                return NullError(nameof(exit));
            if (!TryGetComponent(out SphereCollider col))
                return NullError("SphereCollider");

            if (col.radius <= 0)
                return Error("포탈 입구의 크기는 0보다 커야 합니다.");
            if (!col.isTrigger)
                return TriggerError(col);

            var distance = Vector3.Distance(exit.position, transform.position);
            if (distance < col.radius + 1)
                return Error("포탈 입구와 출구가 겹칩니다. 입구에서 최소 1m 이상 떨어지게 해 주세요.");

            
            // todo : 이동할 블록이 프라이빗 블록인지 검사
            return base.ValidationCheck();
        }
#endif
    }
}