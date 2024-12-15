using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiGame.Services;
using Plugin.Maui.Pedometer;

namespace MauiGame.PageModels;
public partial class MainPageModel : ObservableObject
{
    [ObservableProperty]
    private int _currentSteps = 0;

    [ObservableProperty]
    private string _log = "";

    private readonly IPedometer _pedometer;
    private readonly IBackgroundService _backgroundService;

    private int _startSteps = 0;

    public MainPageModel(IPedometer pedometer, IBackgroundService backgroundService)
    {
        Load();

        _pedometer = pedometer;
        _backgroundService = backgroundService;
    }

    [RelayCommand]
    private void StartPedometer()
    {
        if (_pedometer.IsSupported && !_pedometer.IsMonitoring)
        {
            _pedometer.ReadingChanged += (sender, reading) =>
            {
                CurrentSteps = _startSteps + reading.NumberOfSteps;

                if (_backgroundService.IsActive) _backgroundService.SetTitle(CurrentSteps.ToString());

                WriteLogLine("reading.NumberOfSteps: " + reading.NumberOfSteps);
            };

            _pedometer.Start();

            WriteLogLine("Pedometer started");
        }
    }

    [RelayCommand]
    private void StartBackgroundService()
    {
        _backgroundService.Start(CurrentSteps.ToString());

        WriteLogLine("Background service started");
    }

    [RelayCommand]
    private void StopBackgroundService()
    {
        _backgroundService.Stop();

        WriteLogLine("Background service stopped");
    }

    [RelayCommand]
    private void SaveData()
    {
        DataAccess.Save(CurrentSteps.ToString());

        WriteLogLine("Data saved");
    }

    private void Load()
    {
        var loadedSteps = DataAccess.Load();
        if (int.TryParse(loadedSteps.ToString(), out int steps))
        {
            _startSteps = steps;

            CurrentSteps = _startSteps;

            WriteLogLine("Data loaded");
        }
    }

    private void WriteLogLine(string message)
    {
        if (!string.IsNullOrEmpty(Log)) Log += Environment.NewLine;

        Log +=
            TimeOnly.FromDateTime(DateTime.Now).ToString("O") +
            " - " +
            message;
    }
}
