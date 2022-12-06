using NAudio.Wave;

namespace AudioVisual.Services
{
    public class AudioService
    {
        public double[] AudioValues;
        public double[] FftValues;

        int SampleRate = 44100;
        int BitDepth = 16;
        int ChannelCount = 1;
        int BufferSamples = 4096 / 4;
        public double fftPeriod;

        WasapiLoopbackCapture capture;

        public AudioService()
        {
            capture = new WasapiLoopbackCapture();
            SampleRate = capture.WaveFormat.SampleRate;
            BitDepth = capture.WaveFormat.BitsPerSample;
            ChannelCount = capture.WaveFormat.Channels;

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
            double[] paddedAudio = FftSharp.Pad.ZeroPad(AudioValues);
            double[] fftMag = FftSharp.Transform.FFTpower(paddedAudio);
            Array.Copy(fftMag, FftValues, fftMag.Length);

            // find the frequency peak
            int peakIndex = 0;
            for (int i = 0; i < fftMag.Length; i++)
            {
                if (fftMag[i] > fftMag[peakIndex])
                    peakIndex = i;
            }
            double fftPeriod = FftSharp.Transform.FFTfreqPeriod(SampleRate, fftMag.Length);
            double peakFrequency = fftPeriod * peakIndex;

            // auto-scale the plot Y axis limits
            double fftPeakMag = fftMag.Max();
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
