using MauiGame.Services;
using Plugin.Maui.Pedometer;

namespace MauiGame
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private readonly IPedometer _pedometer;
        private readonly IBackgroundService _backgroundService;

        public MainPage(IPedometer pedometer, IBackgroundService backgroundService)
        {
            InitializeComponent();

            _pedometer = pedometer;
            _backgroundService = backgroundService;

            pedometer.ReadingChanged += (sender, reading) =>
            {
                StepsLabel.Text = reading.NumberOfSteps.ToString();

                LogLabel.Text +=
                    TimeOnly.FromDateTime(DateTime.Now).ToString("O") +
                    " - steps: " +
                    reading.NumberOfSteps +
                    Environment.NewLine;
            };

            pedometer.Start();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void StartBackgroundService_Clicked(object sender, EventArgs e)
        {
            _backgroundService.Start();

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
