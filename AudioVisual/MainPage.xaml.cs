using AudioVisual.Services;

namespace AudioVisual;

public partial class MainPage : ContentPage
{
    private readonly AppViewModel _vm;
    private bool isRunning = true;
    private Thread refreshThread;
    private Thread ledRefreshThread;

    public MainPage(AppViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
        refreshThread = new Thread(new ThreadStart(run));
        refreshThread.Start();

        ledRefreshThread = new Thread(new ThreadStart(runLeds));
        ledRefreshThread.Start();

        TxtComPort.Text = Config.ArduinoComPort;
        TxtFalloff.Text = Config.FalloffSpeed.ToString();
        TxtHiColor.Text = Config.LevelColors[1].ToHex();
        TxtHiPass.Text = Config.HiPass.ToString();
        TxtLowColor.Text = Config.LevelColors[0].ToHex();
        TxtLowPass.Text = Config.LowPass.ToString();
        TxtHiOffset.Text = Config.HiLevelOffset.ToString();
        TxtLowOffset.Text = Config.LowLevelOffset.ToString();
        TxtLedsRefreshRate.Text = Config.LedsRefreshRate.ToString();
        TxtScreenRefreshRate.Text = Config.ScreenRefreshRate.ToString();
    }

    protected override void OnDisappearing()
    {
        isRunning = false;
        refreshThread = null;
        _vm.StopAudio();
        base.OnDisappearing();
    }

    private void run()
    {
        while (isRunning)
        {
            double[] values = _vm.GetFFT();
            drawable2.values = Config.FreqConfig.Frequencies.Select(f => f.CurrentValue).ToArray();
            // drawable.values = _vm.GetAudio().Select(v => (v * 500)).ToArray();
            drawable3.values = values;
            try
            {
                GraphicsV2.Invalidate();
                //GraphicsV.Invalidate();
                GraphicsV3.Invalidate();
            }
            catch (Exception)
            {

            }
            Thread.Sleep(1000 / Config.ScreenRefreshRate);
        }
    }

    private void runLeds()
    {
        while (isRunning)
        {
            double[] values = _vm.GetFFT();
            Config.FreqConfig.Frequencies.ForEach(f =>
            {
                f.SetCurrentValue(f.GetNormalizedValue(_vm.GetFFTPeriod(), values), Config.FalloffSpeed);
            });

            var ledsValues = Config.FreqConfig.Frequencies.Select(f => f.CurrentValue).ToArray();
            _vm.UpdateLEDS(ledsValues);

            Thread.Sleep(1000 / Config.LedsRefreshRate);
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Config.ArduinoComPort = TxtComPort.Text;
        Config.FalloffSpeed = double.Parse(TxtFalloff.Text);
        Config.LevelColors[1] = Color.FromArgb(TxtHiColor.Text);
        Config.HiPass = int.Parse(TxtHiPass.Text);
        Config.LevelColors[0] = Color.FromArgb(TxtLowColor.Text);
        Config.LowPass = int.Parse(TxtLowPass.Text);
        Config.HiLevelOffset = int.Parse(TxtHiOffset.Text);
        Config.LowLevelOffset = int.Parse(TxtLowOffset.Text);
        Config.FreqConfig = FreqConfigs.Leds22X12(Config.LowLevelOffset, Config.HiLevelOffset);
        Config.LedsRefreshRate = int.Parse(TxtLedsRefreshRate.Text);
        Config.ScreenRefreshRate = int.Parse(TxtScreenRefreshRate.Text);
        _vm.UpdateArduinoService();
    }
}

