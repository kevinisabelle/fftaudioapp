using NAudio.Wave;

namespace AudioVisual.Services
{
    public class AudioService
    {
        public double[] AudioValues;
        public double[] FftValues;

        public int SampleRate = 44100;
        int BufferSamples = 4096 / 2;
        public double fftPeriod;

        WasapiLoopbackCapture capture;

        public AudioService()
        {
            capture = new WasapiLoopbackCapture();
            SampleRate = capture.WaveFormat.SampleRate;
            var BitDepth = capture.WaveFormat.BitsPerSample;
            var ChannelCount = capture.WaveFormat.Channels;

            AudioValues = new double[BufferSamples];
            double[] paddedAudio = FftSharp.Pad.ZeroPad(AudioValues);
            double[] fftMag = FftSharp.Transform.FFTpower(paddedAudio);
            FftValues = new double[fftMag.Length];

            fftPeriod = FftSharp.Transform.FFTfreqPeriod(SampleRate, fftMag.Length);
            capture.DataAvailable += WaveIn_DataAvailable;
        }

        void WaveIn_DataAvailable(object? sender, NAudio.Wave.WaveInEventArgs e)
        {
            var buffer = new WaveBuffer(e.Buffer);
            var buffer2 = buffer.FloatBuffer.Select(f => (double)f).Take(BufferSamples * 2).ToArray();

            int count = 0;
            foreach (var val in buffer2)
            {
                if (count % 2 == 0)
                {
                    AudioValues[count / 2] = val;
                }
                count++;
            }

            RefreshFFT();
        }

        public void RefreshFFT()
        {
            var window = new FftSharp.Windows.Hanning();
            var lowPassed = FftSharp.Filter.LowPass(AudioValues, 44100, Config.LowPass);
            var hiPassed = FftSharp.Filter.HighPass(lowPassed, 44100, Config.HiPass);
            double[] paddedAudio = FftSharp.Pad.ZeroPad(hiPassed);
            double[] windowed = window.Apply(paddedAudio);
            double[] fftMag = FftSharp.Transform.FFTpower(windowed);

            Array.Copy(fftMag, FftValues, fftMag.Length);

            fftPeriod = FftSharp.Transform.FFTfreqPeriod(SampleRate, fftMag.Length);
        }

        public void Start()
        {
            capture.StartRecording();
        }

        public void Stop()
        {
            capture.StopRecording();
        }
    }
}
