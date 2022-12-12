using AudioVisual.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AudioVisual
{
    public partial class AppViewModel : ObservableObject
    {
        private ArduinoService _arduinoService;
        private AudioService _audioService;

        public AppViewModel()
        {
            _arduinoService = new ArduinoService(Config.ArduinoComPort);
            _audioService = new AudioService();
        }

        public void UpdateLEDS(double[] values)
        {
            var colors = new List<Color>();

            var ledsPerFreq = Config.NbLeds / values.Length;
            var valuesRev = values.Reverse().ToArray();

            for (int i = 0; i < valuesRev.Length; i++)
            {
                bool inverted = i % 2 != 0;
                colors.AddRange(ColorHelper.GetColorsForValue(valuesRev[i], ledsPerFreq, inverted));
            }

            _arduinoService.SendLightData(colors);
        }

        [RelayCommand]
        public void StartAudio()
        {
            _audioService.Start();
        }

        [RelayCommand]
        public void RefreshFFT()
        {
            _audioService.RefreshFFT();
        }

        public double GetFFTPeriod()
        {
            return _audioService.fftPeriod;
        }

        public double GetSampleRate()
        {
            return _audioService.SampleRate;
        }

        public double[] GetFFT()
        {
            return _audioService.FftValues;
        }

        public double[] GetAudio()
        {
            return _audioService.AudioValues;
        }

        [RelayCommand]
        public void StopAudio()
        {
            _audioService.Stop();
        }
    }
}
