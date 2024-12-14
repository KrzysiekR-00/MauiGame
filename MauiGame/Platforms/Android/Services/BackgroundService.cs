using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using MauiGame.Services;

namespace MauiGame.Platforms.Android.Services;

[Service]
public class BackgroundService : Service, IBackgroundService
{
    public bool IsActive { get; private set; } = false;

    public override IBinder OnBind(Intent intent)
    {
        throw new NotImplementedException();
    }

    [return: GeneratedEnum]
    public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
    {
        if (intent.Action == "START_SERVICE")
        {
            RegisterNotification();
        }
        else if (intent.Action == "STOP_SERVICE")
        {
            StopForeground(true);
            StopSelfResult(startId);
        }

        return StartCommandResult.NotSticky;
    }

    public void Start(string title)
    {
        Intent startService = new Intent(MainActivity.ActivityCurrent, typeof(BackgroundService));
        startService.SetAction("START_SERVICE");
        MainActivity.ActivityCurrent.StartService(startService);

        IsActive = true;
    }

    public void Stop()
    {
        Intent stopIntent = new Intent(MainActivity.ActivityCurrent, this.Class);
        stopIntent.SetAction("STOP_SERVICE");
        MainActivity.ActivityCurrent.StartService(stopIntent);

        IsActive = false;
    }

    public void SetTitle(string title)
    {

    }

    private void RegisterNotification()
    {
        NotificationChannel channel = new NotificationChannel("ServiceChannel", "ServiceDemo", NotificationImportance.Max);
        NotificationManager manager = (NotificationManager)MainActivity.ActivityCurrent.GetSystemService(Context.NotificationService);
        manager.CreateNotificationChannel(channel);
        Notification notification = new Notification.Builder(this, "ServiceChannel")
           .SetContentTitle("Service Working")
           .SetSmallIcon(Resource.Drawable.abc_ab_share_pack_mtrl_alpha)
           .SetOngoing(true)
           .Build();

        StartForeground(100, notification);
    }
}