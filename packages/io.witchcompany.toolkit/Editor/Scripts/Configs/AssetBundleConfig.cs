﻿using System.IO;

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
        public const string Android = "android";
        public const string Ios = "ios";
        
        public const string SuccessMsg = "Upload Success\n\n블록을 서버에 업로드했습니다";
        public const string FailedMsg = "Upload Failed\n\n블록을 서버에 업로드하지 못했습니다\n다시 시도해주세요";
        public const string DuplicationMsg = "Upload Failed\n\n같은 pathName이 존재합니다\npathName 변경 후 다시 시도해주세요";
        public const string FailedKeyMsg = "Upload Failed\n\n블록을 서버에 업로드했으나 랭킹보드의 key를 업로드하지 못했습니다. 개발팀에게 문의주세요";
        public const string PermissionMsg = "Upload Failed\n\n블록 업로드 권한이 없습니다";
        public const string AuthMsg = "Upload Failed\n\n로그인 후 다시 시도해주세요";
    }
}