using UnityEngine;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Runtime.Base;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.Event
{
    public class WitchRespawn : WitchMovementBase
    {
        public override string BehaviourName => "이동: 리스폰";
        public override string Description => "플레이어의 위치를 스폰 포인트로 이동시키는 요소입니다.\n" +
                                              "Y축 제한 여부를 활성화하면 플레이어의 Y축 위치가 제한값 이하일 때 리스폰됩니다.\n"+
                                              "Y축 제한 설정을 하지 않으면 오브젝트에 isTrigger가 활성화 된 Collider가 필요합니다.";
        public override string DocumentURL => "";

        [Header("Y축 제한 여부"), SerializeField]
        private bool isLimitY;
        [Header("Y축 제한값"), SerializeField]
        private float limitY;

        public bool IsLimitY => isLimitY;
        public float LimitY => limitY;
        
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (!TryGetComponent(out Collider col) && !isLimitY)
                return NullError("Collider");

            if (!col.isTrigger) return TriggerError(col);
            
            return base.ValidationCheck();
        }
#endif 
    }
}