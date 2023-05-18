using System.Collections.Generic;
using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchObjectActivator : WitchBehaviour
    {
        public override string BehaviourName => "Key값에 따라 오브젝트 기능 활성화/비활성화";

        public override string Description => "Key에 해당하는 값을 지정할 수 있는 요소입니다.";

        public override string DocumentURL => "";

        [Header("비교할 값들")] 
        [SerializeField] private List<WitchDataComparatorSO> comparators ;
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (comparators.Count <= 0)
                return Error("비교할 값을 설정해주세요!");
            for (var i = 0; i < comparators.Count; i++)
            {
                var comp = comparators[i];
                if (comp == null) return Error($"{i}번째 비교자가 null입니다.");
            }

            return base.ValidationCheck();
        }
#endif
    }
}
