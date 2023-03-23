using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    [RequireComponent(typeof(Collider))]
    public class WitchPortal : WitchBehaviour
    {
        public override string BehaviourName => "포탈";
        public override string Description => "다른 블록과 연결되는 통로입니다.\n" +
                                              "1.해당 컴포넌트가 부착된 오브젝트가 입구입니다. 콜라이더가 필요합니다.\n" +
                                              "2.다른 오브젝트로 출구를 지정해야 합니다. 입구의 콜라이더와 겹치면 안됩니다.";
        public override string DocumentURL => "";

        [Header("출구")]
        [SerializeField] private Transform exit;
    }
}