using UnityEngine;
using UnityEngine.UI;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    [RequireComponent(typeof(Canvas))]
    public class WitchPostItWall : WitchBehaviourUnique
    {
        public override string BehaviourName => "전시: 포스트잇 벽";

        public override string Description => "포스트잇을 붙일 수 있는 벽입니다.\n" +
                                              "World Space 캔버스가 필요합니다.\n" +
                                              "캔버스의 사이즈가 포스트잇을 붙일 수 있는 공간의 사이즈입니다.\n" +
                                              @"'전시: 포스트잇 생성 버튼'이 씬에 있어야 합니다.";

        public override string DocumentURL => "";
        public override int MaximumCount => 1;
        
#if UNITY_EDITOR
        private void Reset()
        {
            if (!TryGetComponent(out Canvas canvas)) 
                canvas = gameObject.AddComponent<Canvas>();
            if (!TryGetComponent(out CanvasScaler _))
                gameObject.AddComponent<CanvasScaler>();
            if (!TryGetComponent(out GraphicRaycaster _))
                gameObject.AddComponent<GraphicRaycaster>();
            
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.sortingLayerName = "Default";
            canvas.sortingOrder = 0;
    
            transform.position = Vector3.zero;
            transform.localScale = Vector3.one * 0.01f;
        }

        public override ValidationError ValidationCheck()
        {
            if (!TryGetComponent(out Canvas canvas))
                return NullError(nameof(Canvas));
            if (canvas.renderMode != RenderMode.WorldSpace)
                return Error("캔버스는 worldSpace여야 합니다.");
            if (Vector3.Distance(transform.localScale,  Vector3.one * 0.01f) > 0.001f)
                return Error("캔버스의 스케일은 0.01이어야 합니다.");
            if (FindObjectOfType<WitchPostItCreator>() == null)
                return Error("'전시: 포스트잇 생성 버튼'이 씬에 있어야 합니다. 생성해주세요.");

            return null;
        }
#endif
    }
}