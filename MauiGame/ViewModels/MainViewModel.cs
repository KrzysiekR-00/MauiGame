using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiGame.DataAccess;
using MauiGame.Services;

namespace MauiGame.ViewModels;
public partial class MainViewModel : ViewModel
{
    [ObservableProperty]
    private int _currentSteps = 0;

    [ObservableProperty]
    private string _log = "";

    private readonly IPedometerService _pedometerService;
    private readonly IBackgroundService _backgroundService;
    private readonly INotificationService _notificationService;

    private readonly IDataAccess _dataAccess;

    private int _startSteps = 0;

    public MainViewModel(
        INavigationService navigationService,
        IViewModelFactory viewModelFactory,
        IPedometerService pedometerService,
        IBackgroundService backgroundService,
        INotificationService notificationService
        )
        : base(navigationService, viewModelFactory)
    {
        _dataAccess = new FileSystemDataAccess();

        _pedometerService = pedometerService;
        _backgroundService = backgroundService;
        _notificationService = notificationService;

        _pedometerService.StepsRegistered += (steps) =>
        {
            CurrentSteps = _startSteps + steps;

            //if (_backgroundService.IsActive) _backgroundService.SetTitle(CurrentSteps.ToString());
            _notificationService.Show(CurrentSteps.ToString(), CurrentSteps.ToString());

            WriteLogLine("steps: " + steps);
        };

        Load();
    }

    [RelayCommand]
    private void StartPedometer()
    {
        _pedometerService.Start();

        WriteLogLine("Pedometer started");
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

    [RelayCommand]
    private void NavigationTest()
    {
        //_navigationService.NavigateToAsync("//TestView", new Dictionary<string, object> { { "TestParameter", "Navigation test" } });

        NavigationService.NavigateTo(ViewModelFactory.CreateTestViewModel("Test value"));
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
