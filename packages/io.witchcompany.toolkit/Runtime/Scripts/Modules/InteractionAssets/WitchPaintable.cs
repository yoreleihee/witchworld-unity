using System.Collections.Generic;
using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    public class WitchPaintable :WitchBehaviour
    {
        public override string BehaviourName => "페인팅 벽";

        public override string Description => "낙서가 가능한 에셋입니다.";

        public override string DocumentURL => throw new System.NotImplementedException();

        [Header("브러쉬 색상 변경")] 
        public List<WitchPaintableBrushChanger> brushes;
    }
}