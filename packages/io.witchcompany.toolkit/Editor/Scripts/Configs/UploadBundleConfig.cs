using UnityEditor;

namespace WitchCompany.Toolkit.Editor.Configs
{
    public class UploadBundleConfig
    {
        private const string Prefs_BundleFolderPath = "upload_bundle_folder_path";
        private const string Prefs_GltfPath = "upload_bundle_gltf_path";
        private const string Prefs_PartsType = "upload_bundle_parts_type";
        private const string Prefs_DisableBody = "upload_bundle_disable_body";
        public static string BundleFolderPath
        {
            get => EditorPrefs.GetString(Prefs_BundleFolderPath, "");
            set => EditorPrefs.SetString(Prefs_BundleFolderPath, value);
        }
        
        public static string GltfPath
        {
            get => EditorPrefs.GetString(Prefs_GltfPath, "");
            set => EditorPrefs.SetString(Prefs_GltfPath, value);
        }

        public static GearType PartsType
        {
            get => (GearType)EditorPrefs.GetInt(Prefs_PartsType, 0);
            set => EditorPrefs.SetInt(Prefs_PartsType, (int)value);
        }
        
        public static SkinType DisableBody
        {
            get => (SkinType)EditorPrefs.GetInt(Prefs_DisableBody, 0);
            set => EditorPrefs.SetInt(Prefs_DisableBody, (int)value);
        }
    }
}