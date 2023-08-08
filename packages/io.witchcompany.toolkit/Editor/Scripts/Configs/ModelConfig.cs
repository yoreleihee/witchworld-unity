using UnityEditor;

namespace WitchCompany.Toolkit.Editor.Configs
{
    public class ModelConfig
    {
        private const string Prefs_BundleFolderPath = "model_bundle_folder_path";
        private const string Prefs_GltfPath = "model_gltf_path";
        private const string Prefs_ModelType = "model_type";
        private const string Prefs_DisableBodyType = "model_disable_body_type";
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

        public static GearType ModelType
        {
            get => (GearType)EditorPrefs.GetInt(Prefs_ModelType, 0);
            set => EditorPrefs.SetInt(Prefs_ModelType, (int)value);
        }
        
        public static SkinType DisableBodyType
        {
            get => (SkinType)EditorPrefs.GetInt(Prefs_DisableBodyType, 0);
            set => EditorPrefs.SetInt(Prefs_DisableBodyType, (int)value);
        }
    }
}