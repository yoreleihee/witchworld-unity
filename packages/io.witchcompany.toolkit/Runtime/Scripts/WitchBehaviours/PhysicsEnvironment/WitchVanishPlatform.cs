using System;
using UnityEngine;
using WitchCompany.Toolkit.Attribute;

namespace WitchCompany.Toolkit.Module.PhysicsEnvironment
{
    public class WitchVanishPlatform : WitchPhysicsEnvironmentBase
    {
        public override string BehaviourName => "물리 오브젝트: 사라지는 플랫폼";
        public override string Description => "플레이어가 해당 플랫폼을 밟으면 플랫폼이 사라집니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 40;

        public enum VanishCondition
        {
            IsEnter,
            IsStay
        }
        
        [Header("사라지는 조건"), Tooltip("오브젝트가 사라지는 조건을 설정합니다."), SerializeField]
        private VanishCondition trigger = VanishCondition.IsEnter;
        [Header("오브젝트 낙하 여부"), Tooltip("체크하면 오브젝트가 즉시 사라지지 않고 낙하하다가 일정 시간 뒤 사라집니다."), SerializeField]
        private bool isDrop = true;
        [Header("재생성 여부"), Tooltip("체크하면 사라진 오브젝트가 일정 시간 뒤 재생성됩니다."), SerializeField]
        private bool isRepair = true;
        [Header("사라지는 시간"), Tooltip("플레이어가 밟은 상태로 오브젝트가 사라지기까지 걸리는 시간(초)"), SerializeField, Range(0f,10f)]
        private float disappearTime = 1.0f;
        [Header("재생성 시간"), Tooltip("사라진 오브젝트가 재생성 되기까지 걸리는 시간(초)"), SerializeField, Range(3f,30f)] 
        private float repairTime = 3.0f;
        
        [Header("렌더러"), SerializeField]
        private Renderer ren;
        [Header("콜라이더"), SerializeField]
        private Collider col;
        [Header("기본 색상"), SerializeField]
        private Color originColor = Color.white;
        [Header("변화할 색상"), SerializeField]
        private Color warnColor = Color.white;

        public VanishCondition Trigger => trigger;
        public bool IsDrop => isDrop;
        public bool IsRepair => isRepair;
        public float DisappearTime => disappearTime;
        public float RepairTime => repairTime;
        public Renderer Rend => ren;
        public Collider Col => col;
        public Color OriginColor => originColor;
        public Color WarnColor => warnColor;
    }
}