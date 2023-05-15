using UnityEngine;

namespace WitchCompany.Toolkit.Module.PhysicsEnvironment
{
    public class WitchDropPlatform : WitchPhysicsEnvironmentBase
    {
        public override string BehaviourName => "물리 오브젝트: 추락 플랫폼";
        public override string Description => "플레이어가 해당 플랫폼을 밟으면 플랫폼이 낙하합니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 32;
        
        [Header("추락 대기 시간"), Tooltip("플레이어가 밟은 후 오브젝트가 추락하기까지 걸리는 시간(초)"), SerializeField, Range(0f,10f)] 
        private float dropTimer = 1.0f;
        [Header("복구 시간"), Tooltip("추락한 플랫폼이 복구되기까지 걸리는 시간(초)"), SerializeField, Range(5f,30f)] 
        private float repairTimer = 5.0f;

        public float DropTimer => dropTimer;
        public float RepairTimer => repairTimer;
    }
}