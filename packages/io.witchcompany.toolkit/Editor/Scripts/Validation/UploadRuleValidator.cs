using System.IO;
using UnityEngine;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.Tool;

namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class UploadRuleValidator
    {
        /// <summary>
        /// 업로드 관련 유효성 검사
        /// <para>-번들 매니페스트 검사</para>
        /// <para>-번들 용량 검사</para>
        /// </summary>
        public static ValidationReport ValidationCheck(BlockPublishOption option, AssetBundleManifest manifest)
        {
            return new ValidationReport()
                .Append(ValidateBundleManifest(option, manifest))
                .Append(ValidateBundleSize(option));
        }

        public static string ValidateBundleManifest(BlockPublishOption option, AssetBundleManifest manifest)
        {
            var bundles = manifest.GetAllAssetBundles();
            
            if (bundles.Length != 1) 
                return $"잘못된 에셋번들: 번들 개수가 이상합니다.({bundles.Length})";
            if (bundles[0] != option.BundleKey)
                return $"잘못된 에셋번들: 번들 이름이 일치하지 않습니다. 번들({bundles[0]}) 블록({option.BundleKey})";

            return null;
        }

        public static string ValidateBundleSize(BlockPublishOption option)
        {
            var path = Path.Combine(AssetBundleConfig.BundleExportPath, option.BundleKey);
            var sizeByte = AssetTool.GetFileSizeByte(path);

            if (sizeByte > AssetBundleConfig.MaxSizeByte)
                return $"에셋 사이즈가 너무 큽니다. ({CommonTool.ByteToMb(sizeByte, 2)}mb/{CommonTool.ByteToMb(AssetBundleConfig.MaxSizeByte)}mb)";

            return null;
        }
    }
}