using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.DataStructure;

namespace WitchCompany.Toolkit.Editor.Configs
{
    /// <summary> 툴킷에서 입력받은 admin 탭 설정값 </summary>
    public class AdminConfig
    {
        private const string Prefs_UnityKey = "admin_unitykey";
        private const string Prefs_ThumbnailPath = "admin_thumbnail_path";
        private const string Prefs_PathName = "admin_pathname";
        private const string Prefs_BlockNameKr = "admin_blockname_kr";
        private const string Prefs_BlockNameEn = "admin_blockname_en";
        private const string Prefs_Type = "admin_type";

        public static int UnityKeyIndex
        {
            get => EditorPrefs.GetInt(Prefs_UnityKey, 0);
            set => EditorPrefs.SetInt(Prefs_UnityKey, value);
        }
        
        public static string ThumbnailPath
        {
            get => EditorPrefs.GetString(Prefs_ThumbnailPath, "");
            set => EditorPrefs.SetString(Prefs_ThumbnailPath, value);
        }

        public static string PathName
        {
            get => EditorPrefs.GetString(Prefs_PathName, "");
            set => EditorPrefs.SetString(Prefs_PathName, value);
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
        
        public static BlockType Type
        {
            get => (BlockType)EditorPrefs.GetInt(Prefs_Type, 0);
            set => EditorPrefs.SetInt(Prefs_Type, (int)value);
        }
    }
}