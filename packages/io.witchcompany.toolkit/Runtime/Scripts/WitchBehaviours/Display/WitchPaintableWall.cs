using System;
using System.Collections.Generic;
using UnityEngine;
using WitchCompany.Toolkit.Extension;

namespace WitchCompany.Toolkit.Module
{
    [Obsolete("사용 금지!! 미완성 에셋입니다.", true)]
    public class WitchPaintableWall : WitchBehaviourUnique
    {
        public override string BehaviourName => "낙서 벽";

        public override string Description => "유저가 자유롭게 낙서할 수 있는 벽입니다.";

        public override string DocumentURL => "";

        [Header("낙서가 그려질 렌더러"), SerializeField] 
        private Renderer targetRenderer;
        
        [Header("브러쉬 리스트"), SerializeReference] 
        private List<WitchPaintableBrush> brushes;

        [Header("낙서 설정")] 
        [SerializeField] private Vector2Int ratio;
        [SerializeField] private Texture2D baseTexture;
        [SerializeField] private Color baseColor;
        
#if UNITY_EDITOR
        public override bool ValidationCheck()
        {
            if (targetRenderer == null) return false;
            if (!transform.HasChild(targetRenderer)) return false;
            foreach (var brush in brushes)
            {
                if (!transform.HasChild(brush, false)) return false;
            }
            
            return true;
        }
#endif
    }
}