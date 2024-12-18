using Android.App;
using Android.Content;
using Android.OS;
using MauiGame.Services;

namespace MauiGame.Platforms.Android.Services;

[Service]
public class BackgroundService : Service, IBackgroundService
{
    public bool IsActive { get; private set; } = false;

    public void Start()
    {
        Intent intent = new(Platform.AppContext, typeof(BackgroundService));
        Platform.AppContext.StartService(intent);

        IsActive = true;
    }

    public void Stop()
    {
        Intent intent = new(Platform.AppContext, typeof(BackgroundService));
        Platform.AppContext.StopService(intent);

        IsActive = false;
    }

    public override IBinder? OnBind(Intent? intent)
    {
        throw new NotImplementedException();
    }

    public override StartCommandResult OnStartCommand(Intent? intent, StartCommandFlags flags, int startId)
    {
        if (NotificationUtilities.GetNotificationChannelId() != null)
        {
            var notification = NotificationUtilities.CreateNotification(
                "Service working",
                "Service working"
                );

            StartForeground(
                NotificationUtilities.NotificationId,
                notification
                );
        }

        return StartCommandResult.NotSticky;
    }
}
