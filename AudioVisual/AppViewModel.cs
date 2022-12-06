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
            _arduinoService = new ArduinoService();
            _audioService = new AudioService();
        }

        [ObservableProperty]
        public string comPort = "com3";

        [RelayCommand]
        public void SetColors()
        {
            var colors = new List<Color>();
            Random rand = new Random();
            for (int i = 0; i < 30; i++)
            {
                colors.Add(Colors.Teal);
            }

            _arduinoService.SendLightData(colors);
        }

        public void UpdateLEDS(double[] values)
        {
            var colors = new List<Color>();
            Random rand = new Random();

            colors.AddRange(GetColorsForValue(values[1], 15));
            // colors.AddRange(GetColorsForValue(values[3], 8));
            // colors.AddRange(GetColorsForValue(values[5], 7));
            colors.AddRange(GetColorsForValue(values[4], 15));

            _arduinoService.SendLightData(colors);
        }

        private List<Color> GetColorsForValue(double value, int length)
        {
            var result = new List<Color>();
            var position = value * length;
            for (int i = 0; i < length; i++)
            {
                if (position > i)
                {
                    result.Add(new Color((float)value, 1 - ((float)value * 1.0f), 0));
                }
                else if (position == i)
                {
                    result.Add(new Color((float)value, (float)value, (float)value));
                }
                else
                {
                    result.Add(Colors.Black);
                }
            }
            return result;
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
