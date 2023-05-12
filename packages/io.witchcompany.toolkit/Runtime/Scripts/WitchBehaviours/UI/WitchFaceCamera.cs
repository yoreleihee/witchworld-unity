using UnityEngine;
using WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.Event.Base;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    public class WitchFaceCamera : WitchUIBase
    {
        public override string BehaviourName => "UI: 카메라 방향 보기";
        public override string Description => "UI가 카메라 방향을 바라보게 만듭니다.";
        public override string DocumentURL => "";

        
        [Header("UI Transform"), SerializeField]
        private Transform uiTransform;
        
        public Transform UITransform => uiTransform;
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            return null;
        }
#endif
    }
}