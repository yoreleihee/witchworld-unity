using System.Collections.Generic;
using UnityEngine;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit
{
    public class WitchLightChanger : WitchBehaviour
    {
        public override string BehaviourName => "라이트 변경";
        public override string Description => "오브젝트 상호작용 시 지정한 Light 색상이 변합니다.\n"+
                                              "WitchPointerEvent가 필요합니다.\n";
        public override string DocumentURL => "";

        [field: Header("연결할 라이트")] 
        [field: SerializeField] public List<Light> ChangeLights { get; private set; }
        
        [field: Header("바꿀 색상 값")]
        [field: SerializeField] public Color LightColor { get; private set; }
        
        [field: Header("색상 변경 속도")]
        [field: SerializeField] public float ChangeSpeed { get; private set; }
        
#if UNITY_EDITOR
        public override ValidationReport ValidationCheckReport()
        {
            var report = new ValidationReport();

            if (ChangeLights == null || ChangeLights.Count == 0)
                return report.Append(NullError($"{nameof(ChangeLights)}"));
            if(GetComponent<WitchPointerEvent>() == null)
                return report.Append(NullError(nameof(WitchPointerEvent)));
            
            return base.ValidationCheckReport();
        }
#endif
    }
}
