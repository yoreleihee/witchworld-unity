using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    [RequireComponent(typeof(BoxCollider))]
    public class WitchPostItWall : WitchBehaviourUnique
    {
        public override string BehaviourName => "전시: 포스트잇 벽";

        public override string Description => "포스트잇을 붙일 수 있는 벽입니다.\n" +
                                              "IsTrigger가 체크되지 않은 박스 콜라이더가 필요합니다.\n" +
                                              @"'전시: 포스트잇 생성 버튼'이 씬에 있어야 합니다.";

        public override string DocumentURL => "";
        public override int MaximumCount => 1;
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (!TryGetComponent(out BoxCollider col))
                return NullError(nameof(BoxCollider));
            if (col.isTrigger)
                return Error("콜라이더는 IsTrigger 체크될 수 없습니다.");
            if (FindObjectOfType<WitchPostItCreator>() == null)
                return Error("'전시: 포스트잇 생성 버튼'이 씬에 있어야 합니다. 생성해주세요.");

            return null;
        }
#endif
    }
}