using MauiGame.Services;

namespace MauiGame.DataAccess;
internal class Pedometer : IPedometerService
{
    public bool IsActive { get; }

    public Action<int> StepsRegistered { get; }

    public void Start()
    {

    }

    public void Stop()
    {

    }
}
