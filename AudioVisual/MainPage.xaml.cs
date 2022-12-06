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

    int[] Frequencies = new int[]
    {
        20,
        65,
        120,
        200,
        300,
        400,
        500,
        600,
        700,
        800,
        900,
        1000,
        2000,
        3000,
        4000,
        5000,
        6000,
        7000,
        8000,
        9000,
        10000,
        12000,
        16000,
        18000
    };

    private void run()
    {
        /*Frequencies = new int[100];
        for (int i = 1; i <= 100; i++)
        {
            int freq = (int)Math.Pow(i, 2) * 2 + 20;
            Frequencies[i - 1] = freq;
        }*/

        while (isRunning)
        {
            double[] values = _vm.GetFFT();
            drawable2.values = Frequencies.Select(f => GetAggregateValue(f, values)).ToArray();
            _vm.UpdateLEDS(drawable2.values);
            try
            {
                GraphicsV2.Invalidate();
            }
            catch (Exception)
            {

            }
            Thread.Sleep(1000 / 60);
        }
    }

    private double GetAggregateValue(int frequency, double[] fftData)
    {
        int arrayPosition = (int)(frequency / _vm.GetFFTPeriod());
        double valuesNeeded = ((frequency / _vm.GetFFTPeriod()));
        double arrayDb = fftData[arrayPosition];
        double normalized = Math.Log(valuesNeeded, 7);

        return ((90 - Math.Abs(arrayDb)) / 90);
    }

    private void drawButtonClicked(object sender, EventArgs e)
    {
        double[] values = _vm.GetFFT();
        drawable.values = values;
        GraphicsV.Invalidate();
    }
}

