using System.IO;

namespace WitchCompany.Toolkit.Editor.Configs
{
    public static class AssetBundleConfig
    {
        public static readonly string BundleString = "Bundles";
        // 에셋번들 익스포트 경로
        public static readonly string BundleExportPath = Path.Combine(".", "WitchToolkit", BundleString); 
        // 소문자로 시작, 소문자+숫자+언더바로 구성된 20자 이내 문자열 규칙
        public const string ValidNameRegex = @"^[a-z][a-z0-9_]{0,19}$";
        public const string ValidBlockNameRegex = @"^.{1,20}$";
        // 최대 에셋번들 사이즈 (128MB)
        public const uint MaxSizeByte = 128 * 1024 * 1024;

        public const string Standalone = "standalone";
        public const string WebGL = "webgl";
    }
}