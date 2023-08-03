using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit
{
    [DisallowMultipleComponent]
    public class WitchGearItem : MonoBehaviour
    {
        [Header("기어 종류")]
        [SerializeField] private GearType gearType;
        [Header("비활성화 할 신체 부위")]
        [SerializeField] private SkinType skinType;
        [Header("아바타의 신장"), Range(0.5f, 2f)]
        [SerializeField] private float height = 1f;
        [Header("Texture를 적용할 Material")]
        [SerializeField] private Material textureMaterial;
        [Header("Color를 적용할 Material")]
        [SerializeField] private Material colorMaterial;

        public GearType GearType => gearType;
        public SkinType SkinType => skinType;
        public float Height => height;
        public Material TextureMaterial => textureMaterial;
        public Material ColorMaterial => colorMaterial;
        
#if UNITY_EDITOR
        private ValidationError Error(string msg) => new(msg, ValidationTag.TagProduct, this);
        
        public ValidationReport ValidationReport()
        {
            var report = new ValidationReport();

            // material 유효성 검사
            if (!transform.GetChild(0).TryGetComponent<SkinnedMeshRenderer>(out var skinnedMeshRenderer))
            {
                report.Append(Error("첫번째 자식 오브젝트에 SkinnedMeshRenderer가 없습니다."));
                return report;
            }
            
            var childrenMaterials = skinnedMeshRenderer.sharedMaterials;

            if (textureMaterial != null && !Array.Exists(childrenMaterials, mat => mat == textureMaterial))
                report.Append(Error("TextureMaterial은 첫번째 자식 오브젝트의 SkinnedMeshRenderer에 포함되어 있는 Material이어야 합니다."));
            
            if (colorMaterial != null && !Array.Exists(childrenMaterials, mat => mat == colorMaterial))  
                report.Append(Error("ColorMaterial은 첫번째 자식 오브젝트의 SkinnedMeshRenderer에 포함되어 있는 Material이어야 합니다."));

            return report;
        }
#endif
    }
}