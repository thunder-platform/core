using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Thunder.Platform.Core.Helpers
{
    public static class ColorConverter
    {
        public const string HexColorFormat = @"^#([a-zA-Z0-9]){1,6}$";

        /// <summary>
        /// To convert hex color with format <see cref="HexColorFormat"/> to Argb string.
        /// </summary>
        /// <param name="hexColor">Hex color with format <see cref="HexColorFormat"/>.</param>
        /// <returns>Returns empty string if format is not valid, otherwise return Argb string value.</returns>
        public static string ConvertHexToArgbString(string hexColor)
        {
            if (!Regex.IsMatch(hexColor, HexColorFormat, RegexOptions.Compiled))
            {
                return string.Empty;
            }

            Color color = Color.FromArgb(int.Parse(hexColor.Replace("#", string.Empty), NumberStyles.AllowHexSpecifier));
            return $"{color.R},{color.G},{color.B}";
        }

        public static string ConvertArgbToHexString(int red, int green, int blue)
        {
            var color = Color.FromArgb(red, green, blue);
            return $"{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        public static Color ConvertHexToArgbColor(string hexColor)
        {
            if (!Regex.IsMatch(hexColor, HexColorFormat, RegexOptions.Compiled))
            {
                return Color.Empty;
            }

            return Color.FromArgb(int.Parse(hexColor.Replace("#", string.Empty), NumberStyles.AllowHexSpecifier));
        }
    }
}
