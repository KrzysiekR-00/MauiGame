namespace MauiGame.Services;

public interface IBackgroundService
{
    bool IsActive { get; }

    void Start(string title);
    void Stop();
    void SetTitle(string title);
}
