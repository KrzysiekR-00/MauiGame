using MauiGame.Services;
using Plugin.Maui.Pedometer;

namespace MauiGame
{
    public partial class MainPage : ContentPage
    {
        private readonly IPedometer _pedometer;
        private readonly IBackgroundService _backgroundService;

        private int _startSteps = 0;
        private int _currentSteps = 0;

        public MainPage(IPedometer pedometer, IBackgroundService backgroundService)
        {
            InitializeComponent();

            Load();

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
                    _currentSteps = _startSteps + reading.NumberOfSteps;

                    StepsLabel.Text = _currentSteps.ToString();

                    if (_backgroundService.IsActive) _backgroundService.SetTitle(_currentSteps.ToString());

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
            _backgroundService.Start(_currentSteps.ToString());

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

        private void Save_Clicked(object sender, EventArgs e)
        {
            DataAccess.Save(_currentSteps.ToString());

            LogLabel.Text +=
                TimeOnly.FromDateTime(DateTime.Now).ToString("O") +
                " - data saved" +
                Environment.NewLine;
        }

        private void Load()
        {
            var loadedSteps = DataAccess.Load();
            if (int.TryParse(loadedSteps.ToString(), out int steps))
            {
                _startSteps = steps;

                LogLabel.Text +=
                TimeOnly.FromDateTime(DateTime.Now).ToString("O") +
                " - data loaded" +
                Environment.NewLine;

                _currentSteps = _startSteps;

                StepsLabel.Text = _currentSteps.ToString();
            }
        }
    }

}
