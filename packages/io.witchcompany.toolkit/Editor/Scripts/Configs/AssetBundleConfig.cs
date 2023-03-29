using System.IO;

namespace WitchCompany.Toolkit.Editor.Configs
{
    public static class AssetBundleConfig
    {
        // 에셋번들 익스포트 경로
        public static readonly string BuildExportPath = Path.Combine(".", "WitchToolkit", "Bundles"); 
        // 소문자로 시작, 소문자+숫자+언더바로 구성된 12자 이내 문자열 규칙
        public const string ValidNameRegex = @"^[a-z][a-z0-9_]{0,11}$";
        // 최대 에셋번들 사이즈 (128byte)
        public const uint MaxSizeByte = 128 * 1024 * 1024;
    }
}