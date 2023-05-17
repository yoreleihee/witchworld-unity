using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.DataStructure;

namespace WitchCompany.Toolkit.Editor.Configs
{
    /// <summary> 툴킷에서 입력받은 admin 탭 설정값 </summary>
    public class AdminConfig
    {
        private const string Prefs_UnityKeyIndex = "admin_unitykey_index";
        private const string Prefs_PathName = "admin_pathname";
        private const string Prefs_ThumbnailPath = "admin_thumbnail_path";
        private const string Prefs_BlockNameKr = "admin_blockname_kr";
        private const string Prefs_BlockNameEn = "admin_blockname_en";
        private const string Prefs_Type = "admin_type";
        private const string Prefs_Theme = "admin_theme";
        private const string Prefs_Level = "admin_level";
        private const string Prefs_DescriptionKr = "admin_description_kr";
        private const string Prefs_DescriptionEn = "admin_description_en";

        public static int UnityKeyIndex
        {
            get => EditorPrefs.GetInt(Prefs_UnityKeyIndex, 0);
            set => EditorPrefs.SetInt(Prefs_UnityKeyIndex, value);
        }
        
        public static string PathName
        {
            get => EditorPrefs.GetString(Prefs_PathName, "");
            set => EditorPrefs.SetString(Prefs_PathName, value);
        }
        
        public static string ThumbnailPath
        {
            get => EditorPrefs.GetString(Prefs_ThumbnailPath, "");
            set => EditorPrefs.SetString(Prefs_ThumbnailPath, value);
        }

        public static string BlockNameKr
        {
            get => EditorPrefs.GetString(Prefs_BlockNameKr, "");
            set => EditorPrefs.SetString(Prefs_BlockNameKr, value);
        }
        
        public static string BlockNameEn
        {
            get => EditorPrefs.GetString(Prefs_BlockNameEn, "");
            set => EditorPrefs.SetString(Prefs_BlockNameEn, value);
        }
        
        public static BlockType BlockType
        {
            get => (BlockType)EditorPrefs.GetInt(Prefs_Type, 0);
            set => EditorPrefs.SetInt(Prefs_Type, (int)value);
        }
        
        public static BlockTheme BlockTheme
        {
            get => (BlockTheme)EditorPrefs.GetInt(Prefs_Theme, 0);
            set => EditorPrefs.SetInt(Prefs_Theme, (int)value);
        }
        
        public static BlockLevel BlockLevel
        {
            get => (BlockLevel)EditorPrefs.GetInt(Prefs_Level, 0);
            set => EditorPrefs.SetInt(Prefs_Level, (int)value);
        }
        
        public static string BlockDescriptionKr
        {
            get => EditorPrefs.GetString(Prefs_DescriptionKr, "");
            set => EditorPrefs.SetString(Prefs_DescriptionKr, value);
        }
        
        public static string BlockDescriptionEn
        {
            get => EditorPrefs.GetString(Prefs_DescriptionEn, "");
            set => EditorPrefs.SetString(Prefs_DescriptionEn, value);
        }
        
        // public static string UnityKeyList
        // {
        //     get => EditorPrefs.GetString(Prefs_UnityKeyList, "");
        //     set => EditorPrefs.SetString(Prefs_UnityKeyList, value);
        // }
        //
        // public static string PopupUnityKeyList
        // {
        //     get => EditorPrefs.GetString(Prefs_PopupUnityKeyList, "");
        //     set => EditorPrefs.SetString(Prefs_PopupUnityKeyList, value);
        // }
    }
}