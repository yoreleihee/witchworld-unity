using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit
{
    [DisallowMultipleComponent]
    public class WitchGearItem : MonoBehaviour
    {
        [Header("기어 종류")]
        [SerializeField] private GearType gearType;
        [Header("Color")]
        [SerializeField] private Renderer colorRenderer;
        [SerializeField] private Material colorMaterial;
        [Header("Texture")]
        [SerializeField] private Renderer textureRenderer;
        [SerializeField] private Material textureMaterial;

        [Header("아바타 설정")]
        [SerializeField, ShowIf(nameof(gearType), GearType.AccessoryBodySuit), Range(0.5f, 2f)]
        private float height = 1f;
        [SerializeField, ShowIf(nameof(gearType), GearType.AccessoryBodySuit)]
        private Avatar avatar;
        
        public GearType GearType => gearType;
        public Renderer ColorRenderer => colorRenderer;
        public Material ColorMaterial => colorMaterial;
        public Renderer TextureRenderer => textureRenderer;
        public Material TextureMaterial => textureMaterial;
        public float Height => height;
        public Avatar Avatar => avatar;
        
#if UNITY_EDITOR
        private ValidationError Error(string msg) => new(msg, ValidationTag.TagProduct, this);
        
        public ValidationReport ValidationReport()
        {
            var report = new ValidationReport();

            if (colorRenderer != null)
            {
                var colorMaterials = colorRenderer.sharedMaterials;
                if (colorMaterial == null || !Array.Exists(colorMaterials, mat => mat == colorMaterial))
                    report.Append(Error("ColorMaterial은 ColorRenderer에 포함되어 있는 Material이어야 합니다."));
            }

            if (textureRenderer != null)
            {   
                var textureMaterials = textureRenderer.sharedMaterials;
                if (textureMaterial == null || !Array.Exists(textureMaterials, mat => mat == textureMaterial))
                    report.Append(Error("TextureMaterial은 TextureRenderer에 포함되어 있는 Material이어야 합니다."));
            }
            
            if (gearType != GearType.Hand)
            {
                if(!transform.GetChild(0).TryGetComponent(out SkinnedMeshRenderer skinnedMeshRenderer)){
                    report.Append(Error("GearType이 Hand가 아닌 아이템의 첫번째 자식 오브젝트에는 SkinnedMeshRenderer가 있어야 합니다."));
                }
            }
            return report;
        }
#endif
    }
}