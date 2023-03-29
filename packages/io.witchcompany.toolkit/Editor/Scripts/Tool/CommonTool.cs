using System;

namespace WitchCompany.Toolkit.Editor.Tool
{
    public static class CommonTool
    {
        public static float ByteToMb(long targetByte, int decimalPlaces = -1)
        {
            const float rev = 1f / (1024 * 1024);
            return RoundToDecimalPlaces(targetByte * rev, decimalPlaces);
        }
        
        public static float RoundToDecimalPlaces(float value, int decimalPlaces)
        {
            if (decimalPlaces < 0) return value;
            
            var multiplier = (float)Math.Pow(10, decimalPlaces);
            return (float)Math.Round(value * multiplier) / multiplier;
        }
        
        /// <summary>현재 Timestamp를 반환한다.</summary>
        public static long GetCurrentTimeStamp()
        {
            var now = DateTime.Now;
            return ((DateTimeOffset)now).ToUnixTimeSeconds();
        }

        /// <summary>주어진 Datetime을 Timestamp로 반환한다.</summary>
        public static long DateTimeToTimeStamp(DateTime value)
        {
            return ((DateTimeOffset)value).ToUnixTimeSeconds();
        }
        
        /// <summary>주어진 Timestamp를 DateTime으로 변환한다.</summary>
        public static DateTime TimeStampToDateTime(long value)
        {
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dt.AddSeconds(value).ToLocalTime();
        }
    }
}