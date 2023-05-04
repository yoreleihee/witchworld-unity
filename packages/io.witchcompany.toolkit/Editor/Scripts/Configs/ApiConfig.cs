using System.Collections.Generic;

namespace WitchCompany.Toolkit.Editor.Configs
{
    public static class ApiConfig
    {
        /// <summary>서버 URL 가져오기</summary>
        public static string URL(string url) => 
            "https://dev-go.witchworld.io/services/api/" + url;

        /// <summary>토큰 헤더 가져오기</summary>
        public static Dictionary<string, string> TokenHeader(string token) =>
            new() {{"Authorization", $"bearer {token}"}};
        
        public static class ContentType
        {
            public static string Json => "application/json";
        }
    }
}