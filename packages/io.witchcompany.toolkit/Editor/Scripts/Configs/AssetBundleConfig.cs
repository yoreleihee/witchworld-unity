namespace WitchCompany.Toolkit.Editor.Configs
{
    public static class AssetBundleConfig
    {
        public const string BuildExportPath = "./WitchToolkit/Bundles";
        public static readonly string[] InvalidNameChars = { " ", "_", ".", ",", "(", ")", "[", "]", "{", "}", "!", "@", "#", "$", "%", "^", "&", "*", "+", "=", "|", "\\", "/", "?", "<", ">", "`", "~", "'", "\"", ":", ";" };
        public static uint MaxSizeByte = 128 * 1024 * 1024; // 128 MB
    }
}