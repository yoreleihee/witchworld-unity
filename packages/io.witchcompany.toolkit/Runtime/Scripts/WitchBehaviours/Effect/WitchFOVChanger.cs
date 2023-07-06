using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Core;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit
{
    public class WitchFOVChanger : WitchBehaviourUnique
    {
        public override string BehaviourName => "화각(FOV) 바꾸기";
        public override string Description => "화각(FOV)를 바꿀 수 있습니다.\n" +
                                              "기본 카메라의 화각(FOV)는 60 입니다.\n" +
                                              "FOV Offsets에서 좁음/보통/넓음의 각도를 설정할 수 있습니다.\n" +
                                              "화각변경 이벤트 호출 시 화각 설정 방법\n"+
                                              "0 : narrow(좁음)\n"+
                                              "1 : medium(보통)\n"+
                                              "2 : wide(넓음)";
        public override string DocumentURL => "";
        public override int MaximumCount => 1;

        [field: Header("FOV 각도")] 
        [field: SerializeField] public SerializableDictionary<FOVOffset, float> FOVOffsets { get; private set; }

        [field: Header("전환 속도")] 
        [field: SerializeField] public float ChangeSpeed { get; private set; }

        [HideInInspector] public UnityEvent<FOVOffset> onChangeFOV = new();

        public void OnChangeFOV(int offset)
        {
            onChangeFOV.Invoke((FOVOffset)offset);
        }
        
#if UNITY_EDITOR
        public override ValidationReport ValidationCheckReport()
        {
            var report = new ValidationReport();

            if (FOVOffsets == null || FOVOffsets.Count == 0)
                return report.Append(NullError($"{nameof(FOVOffsets)}"));

            return report;
        }
#endif
    }
}
