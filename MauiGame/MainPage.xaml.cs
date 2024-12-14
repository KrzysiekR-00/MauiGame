using Plugin.Maui.Pedometer;

namespace MauiGame
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        readonly IPedometer pedometer;

        public MainPage(IPedometer pedometer)
        {
            InitializeComponent();

            this.pedometer = pedometer;

            pedometer.ReadingChanged += (sender, reading) =>
            {
                StepsLabel.Text = reading.NumberOfSteps.ToString();

                LogLabel.Text +=
                TimeOnly.FromDateTime(DateTime.Now).ToString("O") +
                " - " +
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
    }

}
