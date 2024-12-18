using MauiGame.Services;

namespace MauiGame.Platforms.Windows.Services;
internal class DummyPedometerService : IPedometerService
{
    public bool IsActive { get; private set; } = false;

    public Action<int>? StepsRegistered { get; set; }

    public void Start()
    {
        IsActive = true;
    }

    public void Stop()
    {
        IsActive = false;
    }
}
