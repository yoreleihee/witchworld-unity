using UnityEngine;
using UnityEngine.Serialization;
using WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.Event.Base;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    public class WitchFaceCamera : WitchUIBase
    {
        public override string BehaviourName => "효과: 카메라 방향 보기";
        public override string Description => "오브젝트가 카메라 방향을 바라보게 만듭니다.";
        public override string DocumentURL => "";

        
        [FormerlySerializedAs("Transform")] [Header("Transform"), SerializeField]
        private Transform objectTransform;
        
        public Transform ObjTransform => objectTransform;
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            return null;
        }
#endif
    }
}