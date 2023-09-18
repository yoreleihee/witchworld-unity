using System;
using UnityEngine;
using WitchCompany.Toolkit.Validation;
using WitchCompany.Toolkit.Attribute;

namespace WitchCompany.Toolkit.Module
{
    public class WitchBooth : WitchBehaviourUnique
    {
        public override string BehaviourName => "전시: 부스";
        public override string Description => "구걸/경매를 할 수 있는 부스입니다. 반경 10m 이내에 타 오브젝트가 있으면 겹쳐보일 수 있습니다.";
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchBooth-b4daac7b312f47f2b3167e8c76fb64af?pvs=4";
        public override int MaximumCount => 20;

        [Header("부스 Index")]
        [SerializeField, ReadOnly] private int index;
        [Header("부스 타입")]
        [SerializeField] private BoothType boothType;

        public int Index => index;
        public BoothType BoothType => boothType;

#if UNITY_EDITOR
        
//         타 오브젝트의 콜라이더와 겹치지 않게
//         [SerializeField]
//         private float radius = 10f;        
//         public override ValidationError ValidationCheck()
//         {
//             var colliders = Physics.OverlapSphere(transform.position, radius);
//             foreach (var col in colliders)
//             {
//                 if (col.name == "Plane") continue;
//                 if (col != null) return DistanceError(col);
//             }
//             
//             return null;
//         }
//
//         private void OnDrawGizmos()
//         {
//             Gizmos.color = Color.green;
//             Gizmos.DrawWireSphere(this.transform.position, radius);
//         }
        public void Editor_SetIndex(int idx) => index = idx;
#endif
    }
}