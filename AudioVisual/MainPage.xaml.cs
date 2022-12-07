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
            drawable2.values = FreqConfigs.SimpleConfig().Frequencies.Select(f => GetAggregateValue(f, values)).ToArray();
            _vm.UpdateLEDS(drawable2.values);
            try
            {
                GraphicsV2.Invalidate();
            }
            catch (Exception)
            {

            }
            Thread.Sleep(1000 / 80);
        }
    }

    private double GetAggregateValue(Frequency frequency, double[] fftData)
    {
        if (frequency.FreqRange != null)
        {
            int[] arrayPosition = GetArrayRange(frequency);
            var averageValue = fftData[arrayPosition[0]..arrayPosition[1]].Average();
            double normalized = frequency.Remap(averageValue);
            return normalized / 100;
        }
        else
        {
            int arrayPosition = (int)(frequency.Freq / _vm.GetFFTPeriod());
            double arrayDb = fftData[arrayPosition];
            double normalized = frequency.Remap(arrayDb);

            return normalized / 100;
        }
    }

    private int[] GetArrayRange(Frequency frequency)
    {
        int arrayPosition1 = (int)(frequency.FreqRange[0] / _vm.GetFFTPeriod());
        int arrayPosition2 = (int)(frequency.FreqRange[1] / _vm.GetFFTPeriod());

        return new int[] { arrayPosition1, arrayPosition2 };
    }

    private void drawButtonClicked(object sender, EventArgs e)
    {
        double[] values = _vm.GetFFT();
        drawable.values = values;
        GraphicsV.Invalidate();
    }
}

