using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    [RequireComponent(typeof(Collider))]
    public class WitchPostItCreator : WitchBehaviourUnique
    {
        public override string BehaviourName => "전시: 포스트잇 생성 버튼";
        public override string Description => "포스트잇을 생성할 수 있는 요소입니다.\n" +
                                              "콜라이더가 필요합니다.\n" +
                                              "'전시: 포스트잇 벽'이 씬에 있어야 합니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 1;
        
        [field: Header("유저마다 붙일 수 있는 최대치")]
        [field: SerializeField] public int Limit { get; private set; }
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (FindObjectOfType<WitchPostItWall>() == null)
                return Error("'전시: 포스트잇 벽'이 씬에 있어야 합니다. 생성해주세요.");
            
            if (!TryGetComponent(out Collider col)) 
                return NullError(nameof(Collider));
            if (col.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast")) 
                return RayLayerError(col.gameObject);

            return null;
        }
#endif
    }
}