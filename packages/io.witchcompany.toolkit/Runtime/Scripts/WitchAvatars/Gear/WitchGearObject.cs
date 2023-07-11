using UnityEngine;

namespace WitchCompany.Toolkit
{
    public class WitchGearObject : MonoBehaviour
    {
        [Header("기어 종류")]
        [SerializeField] private GearType gearType;
        [Header("기어 장착 부위")]
        [SerializeField] private SkinType skinType;
        
        public GearType GearType => gearType;
        public SkinType SkinType => skinType;

#if UNITY_EDITOR

#endif
    }
}
