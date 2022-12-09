using AudioVisual.Services;

namespace AudioVisual;

public partial class MainPage : ContentPage
{
    private readonly AppViewModel _vm;
    private bool isRunning = true;
    private Thread refreshThread;

    public MainPage(AppViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
        refreshThread = new Thread(new ThreadStart(run));
        refreshThread.Start();
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
            drawable2.values = FreqConfigs.Single().Frequencies.Select(f => f.GetNormalizedValue(_vm.GetFFTPeriod(), values)).ToArray();
            _vm.UpdateLEDS(drawable2.values);
            drawable.values = _vm.GetAudio().Select(v => (v * 500)).ToArray();
            drawable3.values = values;
            try
            {
                GraphicsV2.Invalidate();
                GraphicsV.Invalidate();
                GraphicsV3.Invalidate();
            }
            catch (Exception)
            {

            }
            Thread.Sleep(1000 / 60);
        }
    }
}

