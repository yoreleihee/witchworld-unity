namespace WitchCompany.Toolkit.Editor.BundleTool
{
    public static class AssetBundleConfig
    {
        public const string BuildExportPath = "./WitchToolkit/Bundles";
        public static readonly string[] InvalidNameChars = { " ", "_", ".", ",", "(", ")", "[", "]", "{", "}", "!", "@", "#", "$", "%", "^", "&", "*", "+", "=", "|", "\\", "/", "?", "<", ">", "`", "~", "'", "\"", ":", ";" };
        public static uint MaxSize = 128 * 1024 * 1024; // 128 MB
    }
}