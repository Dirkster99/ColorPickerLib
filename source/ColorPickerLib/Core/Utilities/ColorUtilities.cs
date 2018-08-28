namespace ColorPickerLib.Core.Utilities
{
    using ColorPickerLib.Primitives;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Media;

    internal static class ColorUtilities
    {
        public static readonly Dictionary<string, Color> KnownColors = GetKnownColors();

        public static string GetColorName(this Color color)
        {
            string colorName = KnownColors.Where(kvp => kvp.Value.Equals(color)).Select(kvp => kvp.Key).FirstOrDefault();

            if (String.IsNullOrEmpty(colorName))
                colorName = color.ToString();

            return colorName;
        }

        /// <summary>
        /// Converts a <seealso cref="Color"/> value into a string expression.
        /// </summary>
        /// <param name="stringToFormat"></param>
        /// <param name="isUsingAlphaChannel"></param>
        /// <returns></returns>
        public static string FormatColorString(string stringToFormat, bool isUsingAlphaChannel)
        {
            if (!isUsingAlphaChannel && (stringToFormat.Length == 9))
                return stringToFormat.Remove(1, 2);

            return stringToFormat;
        }

        /// <summary>
        /// Converts a <seealso cref="Color"/> value into a string expression.
        /// </summary>
        /// <param name="colorToFormat"></param>
        /// <returns></returns>
        public static string GetFormatedColorString(Color? colorToFormat, bool isUsingAlphaChannel )
        {
            if ((colorToFormat == null) || !colorToFormat.HasValue)
                return string.Empty;

            var red = colorToFormat.Value.R;
            var green = colorToFormat.Value.G;
            var blue = colorToFormat.Value.B;

            if (isUsingAlphaChannel == true)
            {
                var opacity = colorToFormat.Value.A;
                return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", opacity, red, green, blue);
            }

            return string.Format("{0:X2}{1:X2}{2:X2}", red, green, blue);
        }

        private static Dictionary<string, Color> GetKnownColors()
        {
            var colorProperties = typeof(Colors).GetProperties(BindingFlags.Static | BindingFlags.Public);
            return colorProperties.ToDictionary(p => p.Name, p => (Color)p.GetValue(null, null));
        }

/*** Replaced with static converter code in HsvColor class
        /// <summary>
        /// Converts an RGB color to an HSV color.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="b"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        public static HsvColor ConvertRgbToHsv(int r, int b, int g)
        {
            double delta, min;
            double h = 0, s, v;

            min = Math.Min(Math.Min(r, g), b);
            v = Math.Max(Math.Max(r, g), b);
            delta = v - min;

            if (v == 0.0)
            {
                s = 0;
            }
            else
                s = delta / v;

            if (s == 0)
                h = 0.0;

            else
            {
                if (r == v)
                    h = (g - b) / delta;
                else if (g == v)
                    h = 2 + (b - r) / delta;
                else if (b == v)
                    h = 4 + (r - g) / delta;

                h *= 60;
                if (h < 0.0)
                    h = h + 360;

            }

            return new HsvColor( h, s, v / 255);
        }

        /// <summary>
        ///  Converts an HSV color to an RGB color.
        /// </summary>
        /// <param name="h"></param>
        /// <param name="s"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Color ConvertHsvToRgb(double h, double s, double v)
        {
            double r = 0, g = 0, b = 0;

            if (s == 0)
            {
                r = v;
                g = v;
                b = v;
            }
            else
            {
                int i;
                double f, p, q, t;

                if (h == 360)
                    h = 0;
                else
                    h = h / 60;

                i = (int)Math.Truncate(h);
                f = h - i;

                p = v * (1.0 - s);
                q = v * (1.0 - (s * f));
                t = v * (1.0 - (s * (1.0 - f)));

                switch (i)
                {
                    case 0:
                        {
                            r = v;
                            g = t;
                            b = p;
                            break;
                        }
                    case 1:
                        {
                            r = q;
                            g = v;
                            b = p;
                            break;
                        }
                    case 2:
                        {
                            r = p;
                            g = v;
                            b = t;
                            break;
                        }
                    case 3:
                        {
                            r = p;
                            g = q;
                            b = v;
                            break;
                        }
                    case 4:
                        {
                            r = t;
                            g = p;
                            b = v;
                            break;
                        }
                    default:
                        {
                            r = v;
                            g = p;
                            b = q;
                            break;
                        }
                }

            }

            return Color.FromArgb(255, (byte)(Math.Round(r * 255)), (byte)(Math.Round(g * 255)), (byte)(Math.Round(b * 255)));
        }
***/
        /// <summary>
        /// Generates a list of colors with hues ranging from 0 to 360 and
        /// a saturation and value of 1.
        /// </summary>
        /// <returns></returns>
        public static List<Color> GenerateHsvSpectrum()
        {
            List<Color> colorsList = new List<Color>(8);

            // list of colors with hues ranging from 0 to 360
            for (int i = 0; i < 29; i++)
            {
//                colorsList.Add(ColorUtilities.ConvertHsvToRgb(i * 12, 1, 1));
                  colorsList.Add(HsvColor.RGBFromHSV(new HsvColor(i * 12, 1, 1)));
            }

            // saturation and value of 1
//            colorsList.Add(ColorUtilities.ConvertHsvToRgb(0, 1, 1));
            colorsList.Add(HsvColor.RGBFromHSV(new HsvColor(0, 1, 1)));

            return colorsList;
        }
    }
}
