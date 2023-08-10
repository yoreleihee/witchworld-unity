using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit
{
    public class WitchMaterialBlinker : WitchBehaviour
    {
        public override string BehaviourName => "효과: 머테리얼 블링크";

        public override string Description => "머테리얼의 색상을 전환하는 효과입니다.\n" +
                                              "1번 색상과 2번 색상이 번갈아가며 보입니다.\n" +
                                              "각 색상의 유지시간을 랜덤으로 정할 수 있습니다.\n" +
                                              "color, emission에 각각 적용될 수 있습니다.\n" +
                                              "Play, Pause 이벤트를 바인딩 할 수 있습니다.\n";

        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchMaterialBlinker-4dfd2e0014c24060a11fa84fcd1c9023?pvs=4";
        
        [field: SerializeField] public ShaderPropertyType Type { get; private set; }
        
        [field: Header("타겟 shared 머테리얼(복수 선택 가능)")]
        [field: SerializeField] public List<Material> TargetMats { get; private set; }

        [field: Header("각 상태별 색상 및 유지시간(최소~최대사이 랜덤)")]
        [field: SerializeField] public MaterialState State01 { get; private set; }
        [field: SerializeField] public MaterialState State02 { get; private set; }

        public UnityAction<bool> PlayAction;

        public void PlayBlink() => PlayAction.Invoke(true);
        public void PauseBlink() => PlayAction.Invoke(false);
        
        public enum ShaderPropertyType
        {
            Emission = 0,
            Color = 1,
            All = 2
        }
   
        [System.Serializable]
        public class MaterialState
        {
            [MinMaxSlider(0f, 10f)] public Vector2 duration = Vector2.one;
            public Color color = Color.white;
        }
        
#if UNITY_EDITOR
        public override ValidationReport ValidationCheckReport()
        {
            var report = new ValidationReport();

            if (TargetMats is not {Count: > 0})
                return report.Append("", ValidationTag.TagScript, this);
            
            return base.ValidationCheckReport();
        }
#endif
    }
}
