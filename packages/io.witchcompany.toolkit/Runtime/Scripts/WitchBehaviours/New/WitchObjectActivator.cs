using System.Collections.Generic;
using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    public class WitchObjectActivator : WitchBehaviour
    {
        public override string BehaviourName => "Key값에 따라 오브젝트 기능 활성화/비활성화";

        public override string Description => "Key에 해당하는 값을 지정할 수 있는 요소입니다.";

        public override string DocumentURL => "";

        [Header("비교할 데이터들")] 
        [SerializeField] private List<WitchDataComparator> comparators ;
    }
}
