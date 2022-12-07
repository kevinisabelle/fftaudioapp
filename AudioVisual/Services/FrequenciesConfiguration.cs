namespace AudioVisual.Services
{
    public class FrequenciesConfiguration
    {
        public List<Frequency> Frequencies { get; set; }
    }

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

        public double Remap(double value)
        {
            double from1 = ValuesRange?[0] ?? -90;
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
    }
}
