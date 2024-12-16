namespace MauiGame.Services;
public interface IPedometerService
{
    bool IsActive { get; }

    Action<int> StepsRegistered { get; }

    void Start();
    void Stop();
}
