using System.Collections.Generic;
using UnityEngine;
using WitchCompany.Toolkit.Extension;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchPaintableWall : WitchBehaviourUnique
    {
        public override string BehaviourName => "낙서 벽";
        public override string Description => "유저가 자유롭게 낙서할 수 있는 벽입니다.\n" +
                                              "낙서가 되는 벽면의 텍스쳐 및 색상은 낙서 설정을 따릅니다.\n" +
                                              "낙서장의 비율은 256,512,1024 중 하나여야 합니다.\n" +
                                              "브러쉬가 없다면 검은색, 있다면 첫브러쉬의 색상으로 시작합니다.";
        public override string DocumentURL => "";

        [Header("낙서가 그려질 렌더러"), SerializeField] 
        private Renderer targetRenderer;
        
        [Header("브러쉬 리스트"), SerializeReference] 
        private List<WitchPaintableBrush> brushes;

        [Header("낙서 설정")] 
        [SerializeField] private Vector2Int ratio;
        [SerializeField] private Texture2D baseTexture;
        [SerializeField] private Color baseColor;

        public Renderer TargetRenderer => targetRenderer;
        public List<WitchPaintableBrush> Brushes => brushes;
        public Vector2Int Ratio => ratio;
        public Texture2D BaseTex => baseTexture;
        public Color BaseColor => baseColor;

#if UNITY_EDITOR
        // public override ValidationError ValidationCheck()
        // {
        //     if (targetRenderer == null) return false;
        //     if (!transform.HasChild(targetRenderer)) return false;
        //     foreach (var brush in brushes)
        //     {
        //         if (!transform.HasChild(brush, false)) return false;
        //     }
        //
        //     if (ratio.x is not (256 or 512 or 1024)) return false;
        //     if (ratio.y is not (256 or 512 or 1024)) return false;
        //     
        //     return true;
        // }
#endif
    }
}