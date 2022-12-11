using CommunityToolkit.Maui.Core.Extensions;

namespace AudioVisual
{
    public static class ColorHelper
    {
        public static List<Color> GetColorsForValue(double value, int length, bool invert = false)
        {
            var result = new List<Color>();
            var position = value * length;
            for (int i = 0; i < length; i++)
            {
                if (position >= i)
                {
                    result.Add(GetFadedColor(Config.LevelColors[0], Config.LevelColors[1], value));
                }
                else
                {
                    result.Add(Colors.Black);
                }
            }
            return result;
        }

        public static Color GetFadedColor(Color start, Color end, double ratio)
        {
            System.Diagnostics.Debug.Assert(ratio >= 0 && ratio <= 1);
            return Color.FromRgb(
                (byte)(start.GetByteRed() + (ratio * (end.GetByteRed() - start.GetByteRed()))),
                (byte)(start.GetByteGreen() + (ratio * (end.GetByteGreen() - start.GetByteGreen()))),
                (byte)(start.GetByteBlue() + (ratio * (end.GetByteBlue() - start.GetByteBlue()))));
        }
    }
}
