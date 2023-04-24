using UnityEngine;
using WitchCompany.Toolkit.Attribute;

namespace WitchCompany.Toolkit.Module.PhysicsEnvironment
{
    public class WitchPhysicsPlatform : WitchPhysicsEnvironmentBase
    {
        public override string BehaviourName => "물리 오브젝트: 플랫폼";
        public override string Description => "다양한 물리적 환경을 구현하는 오브젝트입니다.\n" +
                                              "Rigidbody 없는 콜라이더가 필요합니다.\n";
        public override string DocumentURL => "";
        public override int MaximumCount => 64;

        [Header("타입")] 
        [SerializeField] private WitchEnvironmentType baseType;
        [Space(10)]
        [SerializeField, ShowIf(nameof(baseType), WitchEnvironmentType.Surface)] 
        private WitchSurfaceType surfaceType = WitchSurfaceType.Ice;
        [SerializeField, ShowIf(nameof(baseType), WitchEnvironmentType.Volume)]
        private WitchVolumeType volumeType = WitchVolumeType.Water;
        [SerializeField, ShowIf(nameof(baseType), WitchEnvironmentType.Wall)] 
        private WitchWallType wallType = WitchWallType.Sticky;

        public WitchEnvironmentType Environment => baseType;
        public WitchSurfaceType Surface => surfaceType;
        public WitchVolumeType Volume => volumeType;
        public WitchWallType Wall => wallType;
    }
}