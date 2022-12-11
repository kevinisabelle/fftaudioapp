namespace AudioVisual.Services
{
    public class Frequency
    {
        public Frequency(double[] freqRange, double[] valueRange = null)
        {
            FreqRange = freqRange;
            ValuesRange = valueRange;
        }

        public Frequency(double freq, double[] valuesRange = null)
        {
            Freq = freq;
            ValuesRange = valuesRange;
        }

        public double Freq { get; set; }

        public double[] FreqRange { get; set; } = null;
        public double[] ValuesRange { get; set; } = null;
        public double CurrentValue { get; set; } = 0;

        public void SetCurrentValue(double value, double fallOffSpeed)
        {
            if (CurrentValue - value > fallOffSpeed)
            {
                CurrentValue -= fallOffSpeed;
                return;
            }

            CurrentValue = value;
        }

        public double Remap(double value)
        {
            double from1 = ValuesRange?[0] ?? -100;
            double to1 = ValuesRange?[1] ?? 0;
            double from2 = 0;
            double to2 = 100;
            var result = (value - from1) / (to1 - from1) * (to2 - from2) + from2;

            if (result > to2)
            {
                return to2;
            }

            if (result < from2)
            {
                return from2;
            }

            return result;
        }

        public double GetNormalizedValue(double fftPeriod, double[] fftData)
        {
            if (FreqRange != null)
            {
                int[] arrayPosition = GetArrayRange(fftPeriod);
                var averageValue = fftData[arrayPosition[0]..arrayPosition[1]].Max();
                double normalized = Remap(averageValue);
                return normalized / 100;
            }
            else
            {
                int arrayPosition = (int)(Freq / fftPeriod);
                double arrayDb = fftData[arrayPosition];
                double normalized = Remap(arrayDb);

                return normalized / 100;
            }
        }

        private int[] GetArrayRange(double fftPeriod)
        {
            int arrayPosition1 = (int)(FreqRange[0] / fftPeriod);
            int arrayPosition2 = (int)(FreqRange[1] / fftPeriod);

            return new int[] { arrayPosition1, arrayPosition2 };
        }
    }
}
