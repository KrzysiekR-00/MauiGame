using MauiGame.Services;
using Plugin.Maui.Pedometer;

namespace MauiGame.Platforms.Android.Services;
internal class PedometerService : IPedometerService
{
    private readonly IPedometer _pedometer;

    public bool IsActive { get; private set; }

    public Action<int>? StepsRegistered { get; set; }

    public PedometerService(IPedometer pedometer)
    {
        _pedometer = pedometer;
    }

    public void Start()
    {
        if (_pedometer.IsSupported && !_pedometer.IsMonitoring)
        {
            _pedometer.ReadingChanged += PedometerReadingChanged;

            _pedometer.Start();

            IsActive = true;
        }
    }

    public void Stop()
    {
        if (_pedometer.IsSupported && _pedometer.IsMonitoring)
        {
            _pedometer.ReadingChanged -= PedometerReadingChanged;

            _pedometer.Stop();

            IsActive = false;
        }
    }

    private void PedometerReadingChanged(object? sender, PedometerData pedometerData)
    {
        StepsRegistered?.Invoke(pedometerData.NumberOfSteps);
    }
}
