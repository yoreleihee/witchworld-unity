using UnityEditor;
using WitchCompany.Toolkit.Editor.DataStructure;

namespace WitchCompany.Toolkit.Editor.Configs
{
    /// <summary>
    /// 툴킷 publish 설정
    /// </summary>
    public static class PublishConfig
    {
        private const string Prefs_Scene = "publish_scene";
        private const string Prefs_Theme = "publish_theme";
        private const string Prefs_ThumbnailPath = "publish_thumnail";
        private const string Prefs_Level = "publish_level";
        private const string Prefs_Capacity = "publish_capacity";
        private const string Prefs_Official = "publish_official";
        public static SceneAsset Scene
        {
            get => AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorPrefs.GetString(Prefs_Scene, ""));
            set => EditorPrefs.SetString(Prefs_Scene, AssetDatabase.GetAssetPath(value));
        }
        
        public static string ThumbnailPath
        {
            get => EditorPrefs.GetString(Prefs_ThumbnailPath, "");
            set => EditorPrefs.SetString(Prefs_ThumbnailPath, value);
        }
        
        
        public static BundleTheme Theme
        {
            get => (BundleTheme)EditorPrefs.GetInt(Prefs_Theme, 0);
            set => EditorPrefs.SetInt(Prefs_Theme, (int)value);
        }
        
        public static GameLevel Level
        {
            get => (GameLevel)EditorPrefs.GetInt(Prefs_Level, 0);
            set => EditorPrefs.SetInt(Prefs_Level, (int)value);
        }
        
        public static int Capacity
        {
            get => EditorPrefs.GetInt(Prefs_Capacity, 20);
            set => EditorPrefs.SetInt(Prefs_Capacity, (int)value);
        }

        public static bool Official
        {
            get => EditorPrefs.GetBool(Prefs_Official, false);
            set => EditorPrefs.SetBool(Prefs_Official, value);
        }
    }
}