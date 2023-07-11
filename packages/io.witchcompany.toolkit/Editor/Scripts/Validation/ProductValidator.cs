using System.IO;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.Tool;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class ProductValidator
    {
        private const string FileSizeErrorMsg = "파일 크기 : {0}/{1} KB\n최대 용량을 초과했습니다. 다시 시도해주세요.";
        
        public static ValidationReport ValidationCheck()
        {
            return new ValidationReport().Append(
                ValidationFileSize()
            );
        }

        private static ValidationReport ValidationFileSize()
        {
            var report = new ValidationReport();

            var fileSize = AssetTool.GetFileSizeByte(ProductConfig.PrefabPath);
            
            if (fileSize > ProductConfig.MaxProductSizeKb * 1024)
            {
                var error = new ValidationError(string.Format(FileSizeErrorMsg, fileSize,
                    ProductConfig.MaxProductSizeKb), ValidationTag.TagProduct, ProductConfig.Prefab);

                report.Append(error);
            }
            return report;
        }
        
    }
}