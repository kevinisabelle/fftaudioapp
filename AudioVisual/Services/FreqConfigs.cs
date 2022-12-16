namespace AudioVisual.Services
{
    public static class FreqConfigs
    {
        public static FrequenciesConfiguration Leds22X12(int lowOffset, int hiOffset)
        {
            return new FrequenciesConfiguration()
            {
                Frequencies = new List<Frequency>
                {

                    new(55, new double[] { -50+lowOffset, -2 }),
                    new(73.42, new double[] { -50+lowOffset, -3 }),
                    new(92.5, new double[] { -50+lowOffset, -4 }),
                    new(123.47, new double[]{-50+lowOffset, -5}),
                    new(155.56, new double[]{-60+lowOffset, -6+hiOffset}),
                    new(new double[]{200, 300 }, new double[]{-50+lowOffset,-6+hiOffset}),
                    new(new double[]{300, 400 }, new double[]{-52+lowOffset, -9+hiOffset}),
                    new(new double[]{400, 500 }, new double[]{-54+lowOffset,-15+hiOffset}),
                    new(new double[]{500, 600 }, new double[]{-56+lowOffset,-18+hiOffset}),
                    new(new double[]{600, 800 }, new double[]{-56+lowOffset,-18+hiOffset}),
                    new(new double[]{800, 1000 }, new double[]{-58+lowOffset,-21+hiOffset}),
                    new(new double[]{1000, 2000 }, new double[]{-60+lowOffset,-21+hiOffset}),
                    new(new double[]{2000, 3000 }, new double[]{-62+lowOffset, -22+hiOffset}),
                    new(new double[]{3000, 4000 }, new double[]{-64+lowOffset, -23+hiOffset}),
                    new(new double[]{4000, 5000 }, new double[]{-66+lowOffset, -24+hiOffset}),
                    new(new double[]{5000, 6000 }, new double[]{-68+lowOffset, -26+hiOffset}),
                    new(new double[]{6000, 7000 }, new double[]{-68+lowOffset, -28+hiOffset}),
                    new(new double[]{7000, 9000 }, new double[]{-70+lowOffset, -30+hiOffset}),
                    new(new double[]{10000, 12000 }, new double[]{-72+lowOffset, -30+hiOffset}),
                    new(new double[]{12000, 16000 }, new double[]{-75+lowOffset, -35+hiOffset}),
                    new(new double[]{16000, 18000 }, new double[]{-90+lowOffset, -50+hiOffset}),
                    new(new double[]{18000, 20000 }, new double[]{-100+lowOffset, -55+hiOffset}),

                }
            };
        }


        public static FrequenciesConfiguration SimpleConfig()
        {
            return new FrequenciesConfiguration()
            {
                Frequencies = new List<Frequency>
                {
                    new(36.71),
                    new(51.91),
                    new(55),
                    new(73.42),
                    new(92.5),
                    new(123.47),
                    new(new double[]{150, 200 }, new double[]{-75,-30}),
                    new(new double[]{200, 300 }, new double[]{-75,-30}),
                    new(new double[]{300, 400 }, new double[]{-75,-30}),
                    new(new double[]{400, 500 }, new double[]{-75,-30}),
                    new(new double[]{500, 800 }, new double[]{-75,-30}),
                    new(new double[]{800, 1000 }, new double[]{-75,-30}),
                    new(new double[]{1000, 2000 }, new double[]{-75,-30}),
                    new(new double[]{2000, 3000 }, new double[]{-75,-30}),
                    new(new double[]{3000, 4000 }, new double[]{-75,-30}),
                    new(new double[]{4000, 5000 }, new double[]{-75,-30}),
                    new(new double[]{5000, 6000 }, new double[]{-75,-30}),
                    new(new double[]{6000, 7000 }, new double[]{-75,-30}),
                    new(new double[]{7000, 8000 }, new double[]{-75,-30}),
                    new(new double[]{8000, 9000 }, new double[]{-75,-30}),
                    new(new double[]{10000, 12000 }, new double[]{-75, -30}),
                    new(new double[]{12000, 16000 }, new double[]{-75, -35}),
                    new(new double[]{16000, 18000 }, new double[]{-80, -50}),
                    new(new double[]{18000, 20000 }, new double[]{-100, -55}),
                }
            };
        }

        public static FrequenciesConfiguration MegaSimple()
        {
            return new FrequenciesConfiguration()
            {
                Frequencies = new List<Frequency>
                {
                    new(50),
                    new(new double[]{100, 250 }),
                    new(new double[]{1000, 5000 }, new double[]{-90, -15}),
                    new(new double[]{8000, 10000 }, new double[]{-100, -30}),
                    new(new double[]{12000, 17000 }, new double[]{-100, -35}),
                }
            };
        }

        public static FrequenciesConfiguration Single()
        {
            return new FrequenciesConfiguration()
            {
                Frequencies = new List<Frequency>
                {
                    new(new double[]{30, 70 }, new double[] { -70, -6 }),
                    new(new double[]{200, 500 }, new double[] { -60, -9 }),
                    new(new double[]{8000, 15000 }, new double[] { -70, -15 }),
                }
            };
        }

        public static FrequenciesConfiguration NotesConfig()
        {
            return new FrequenciesConfiguration()
            {
                Frequencies = new List<Frequency>
                {
                    new(36.71),
                    new(51.91),

                    new(73.42),



                    new(92.5),


                    new(116.54),
                    new(123.47),
                    new(130.81),
                    new(138.59),
                    new(146.83),
                    new(155.56),
                    new(164.81),
                    new(174.61),
                    new(185),
                    new(196),
                    new(207.65),
                    new(220),
                    new(233.08),
                    new(246.94),
                    new(261.63),
                    new(277.18),
                    new(293.66),
                    new(311.13),
                    new(329.63),
                    new(349.23),
                    new(369.99),
                    new(392),
                    new(415.3 ),
                    new(440),
                    new(466.16),
                    new(493.88),
                    new(523.25),
                    new(554.37),
                    new(587.33),
                    new(622.25),
                    new(659.25),
                    new(698.46),
                    new(739.99),
                    new(783.99),
                    new(830.61),
                    new(880),
                    new(932.33),
                    new(987.77),
                    new(1046.5),
                    new(1108.73),
                    new(1174.66),
                    new(1244.51),
                    new(1318.51),
                    new(1396.91),
                    new(1479.98),
                    new(1567.98),
                    new(1661.22),
                    new(1760),
                    new(1864.66),
                    new(1975.53),
                    new(2093),
                    new(2217.46),
                    new(2349.32),
                    new(2489.02),
                    new(2637.02),
                    new(2793.83),
                    new(2959.96),
                    new(3135.96),
                    new(3322.44),
                    new(3520),
                    new(3729.31),
                    new(3951.07),
                    new(4186.01),
                    new(4434.92),
                    new(4698.63),
                    new(4978.03),
                    new(5274.04),
                    new(5587.65),
                    new(5919.91),
                    new(6271.93),
                    new(6644.88),
                    new(7040),
                    new(7458.62),
                    new(7902.13)
                }
            };
        }
    }
}
