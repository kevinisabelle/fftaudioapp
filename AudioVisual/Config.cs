using AudioVisual.Services;

namespace AudioVisual
{
    public static class Config
    {
        public static int LedsRefreshRate = 120;
        public static int ScreenRefreshRate = 30;

        public static int LowLevelOffset = -8;
        public static int HiLevelOffset = -8;

        public static FrequenciesConfiguration FreqConfig = FreqConfigs.Leds22X12(LowLevelOffset, HiLevelOffset);

        public static string ArduinoComPort = "COM4";

        public static int HiPass = 35;
        public static int LowPass = 20000;

        public static readonly int NbLeds = 12 * 22;

        public static double FalloffSpeed = 0.075;

        public static readonly List<Color> LevelColors = new List<Color>()
        {
            Color.FromArgb("#00FF00"),
            Color.FromArgb("#FF0000")
        };
    }
}
