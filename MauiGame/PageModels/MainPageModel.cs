using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiGame.DataAccess;
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
    private readonly INotificationService _notificationService;

    private readonly IDataAccess _dataAccess;

    private int _startSteps = 0;

    public MainPageModel(
        IPedometer pedometer,
        IBackgroundService backgroundService,
        INotificationService notificationService
        )
    {
        _dataAccess = new FileSystemDataAccess();

        _pedometer = pedometer;
        _backgroundService = backgroundService;
        _notificationService = notificationService;

        Load();
    }

    [RelayCommand]
    private void StartPedometer()
    {
        if (_pedometer.IsSupported && !_pedometer.IsMonitoring)
        {
            _pedometer.ReadingChanged += (sender, reading) =>
            {
                CurrentSteps = _startSteps + reading.NumberOfSteps;

                //if (_backgroundService.IsActive) _backgroundService.SetTitle(CurrentSteps.ToString());
                _notificationService.Show(CurrentSteps.ToString(), CurrentSteps.ToString());

                WriteLogLine("reading.NumberOfSteps: " + reading.NumberOfSteps);
            };

            _pedometer.Start();

            WriteLogLine("Pedometer started");
        }
    }

    [RelayCommand]
    private void StartBackgroundService()
    {
        //_backgroundService.Start(CurrentSteps.ToString());
        _backgroundService.Start();

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
        _dataAccess.Save(CurrentSteps);

        WriteLogLine("Data saved");
    }

    private void Load()
    {
        if (_dataAccess.TryLoad(out int loadedData))
        {
            _startSteps = loadedData;

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
