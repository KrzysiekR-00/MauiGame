using MauiGame.Services;
using Plugin.Maui.Pedometer;

namespace MauiGame
{
    public partial class MainPage : ContentPage
    {
        //int count = 0;

        private readonly IPedometer _pedometer;
        private readonly IBackgroundService _backgroundService;

        private int _steps = 0;

        public MainPage(IPedometer pedometer, IBackgroundService backgroundService)
        {
            InitializeComponent();

            _pedometer = pedometer;
            _backgroundService = backgroundService;
        }

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}

        private void StartPedometer_Clicked(object sender, EventArgs e)
        {
            if (_pedometer.IsSupported && !_pedometer.IsMonitoring)
            {
                _pedometer.ReadingChanged += (sender, reading) =>
                {
                    _steps = reading.NumberOfSteps;
                    StepsLabel.Text = _steps.ToString();

                    if (_backgroundService.IsActive) _backgroundService.SetTitle(_steps.ToString());

                    LogLabel.Text +=
                        TimeOnly.FromDateTime(DateTime.Now).ToString("O") +
                        " - steps: " +
                        reading.NumberOfSteps +
                        Environment.NewLine;
                };

                _pedometer.Start();
            }
        }

        private void StartBackgroundService_Clicked(object sender, EventArgs e)
        {
            _backgroundService.Start(_steps.ToString());

            LogLabel.Text +=
                TimeOnly.FromDateTime(DateTime.Now).ToString("O") +
                " - start background service" +
                Environment.NewLine;
        }

        private void StopBackgroundService_Clicked(object sender, EventArgs e)
        {
            _backgroundService.Stop();

            LogLabel.Text +=
                TimeOnly.FromDateTime(DateTime.Now).ToString("O") +
                " - stop background service" +
                Environment.NewLine;
        }
    }

}
