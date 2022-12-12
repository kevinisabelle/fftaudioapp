using CommunityToolkit.Maui.Core.Extensions;

namespace AudioVisual
{
    public static class ColorHelper
    {
        public static List<Color> GetColorsForValue(double value, int length, bool invert = false)
        {
            var result = new List<Color>();
            var position = value * length;

            if (!invert)
            {
                for (int i = length - 1; i >= 0; i--)
                {
                    AddColor(result, position, value, i);
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    AddColor(result, position, value, i);
                }
            }

            return result;
        }

        private static void AddColor(List<Color> result, double position, double value, int i)
        {
            if (Math.Round(position) > i)
            {
                result.Add(GetFadedColor(Config.LevelColors[0], Config.LevelColors[1], value));
            }
            else if (Math.Round(position) == i)
            {
                var color = GetFadedColor(Config.LevelColors[0], Config.LevelColors[1], value);
                result.Add(GetFadedColor(color, Colors.Black, 0.75));
            }
            else
            {
                result.Add(Colors.Black);
            }
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
