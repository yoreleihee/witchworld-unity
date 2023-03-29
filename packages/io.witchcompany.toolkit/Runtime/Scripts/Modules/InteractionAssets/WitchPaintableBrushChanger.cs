using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    public class WitchPaintableBrushChanger : WitchBehaviour
    {
        public override string BehaviourName => "페인트 브러쉬";

        public override string Description => "클릭시 페인트 브러쉬 색상을 변경해주는 오브젝트 입니다.";

        public override string DocumentURL => throw new System.NotImplementedException();

        [Header("변경할 색상")] 
        public Color color;

        [Header("클릭 콜라이더")] 
        public Collider clickCollider;
    }
}