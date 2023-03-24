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
    }
}