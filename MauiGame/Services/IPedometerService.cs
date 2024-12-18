namespace MauiGame.Services;
public interface IPedometerService
{
    bool IsActive { get; }

    Action<int>? StepsRegistered { get; set; }

    void Start();
    void Stop();
}
