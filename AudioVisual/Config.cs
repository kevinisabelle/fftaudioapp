﻿using AudioVisual.Services;

namespace AudioVisual
{
    public static class Config
    {
        public static readonly int LedsRefreshRate = 100;
        public static readonly int ScreenRefreshRate = 120;

        public static readonly FrequenciesConfiguration FreqConfig = FreqConfigs.Leds20X12();

        public static readonly string ArduinoComPort = "COM4";

        public static readonly int HiPass = 35;
        public static readonly int LowPass = 18000;

        public static readonly int NbLeds = 12 * 22;

        public static readonly List<Color> LevelColors = new List<Color>()
        {
            Colors.Green,
            Colors.Yellow,
            Colors.Red,
        };
    }
}
