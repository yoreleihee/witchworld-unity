using UnityEngine;

namespace WitchCompany.Toolkit
{
    public class WitchGearObject : MonoBehaviour
    {
        [Header("기어 종류")]
        [SerializeField] private GearType gearType;
        [Header("비활성화 할 신체 부위")]
        [SerializeField] private SkinType skinType;
        [Header("아바타의 신장(m)"), Range(0.5f, 3f)]
        [SerializeField] private float height = 1.5f;
        
        public GearType GearType => gearType;
        public SkinType SkinType => skinType;
        public float Height => height;

#if UNITY_EDITOR

#endif
    }
}
