using UnityEngine;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Extension;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchDisplayFrame : WitchDisplayBase
    {
        public override string BehaviourName => "전시: 액자";
        public override string Description => "유저가 사진,움짤,영상 등을 등록하여 전시할 수 있는 액자입니다.\n" +
                                              "전시 될 미디어 타입에서 사진인지 영상인지 구분해야합니다.\n" +
                                              "프레임 타입에서 배치 될 오브젝트 형식을 정해줘야 합니다.";
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchDisplayFrame-e60b2c85dfc2437290f2327163f19ea4?pvs=4";
        public override int MaximumCount => 40;
        
        [field: Header("전시 될 미디어 타입")]
        [field: SerializeField] public MediaType MediaType { get; private set; }
        
        // [Header("전시물 인덱스"), SerializeField, ReadOnly]
        // private int index;
        // public int Index => index;
        //
        //
        // [field: Header("디스플레이 타입")]
        // [field: SerializeField] public DisplayType DisplayType { get; private set; }
        //
        // [Header("사진,움짤,영상 등이 그려질 랜더러"), SerializeField] 
        // private Renderer mediaRenderer;
        // public Renderer MediaRenderer => mediaRenderer;
        //
        // [Header("유저 클릭에 반응할 콜라이더"), SerializeField]
        // private Collider interactionCollider;
        // public Collider InteractionCollider => interactionCollider;
        

// #if UNITY_EDITOR
//         public override ValidationError ValidationCheck()
//         {
//             if (mediaRenderer == null) return NullError(nameof(mediaRenderer));
//             if (interactionCollider == null) return NullError(nameof(mediaRenderer));
//
//             if (!transform.HasChild(mediaRenderer)) return ChildError(nameof(mediaRenderer));
//             if (!transform.HasChild(interactionCollider)) return ChildError(nameof(interactionCollider));
//
//             //scale 변화를 주는 로직이 있기 때문에 static 체크
//             if (gameObject.isStatic) return StaticError(gameObject);
//             
//             return null;
//         }
//
//         public void Editor_SetIndex(int id) => index = id;
// #endif
    }
}