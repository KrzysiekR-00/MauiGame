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

    private NotificationManager _notificationManager;
    private Notification _notification;

    private int _notificationId = 100; // Unikalny identyfikator powiadomienia

    private string _title = string.Empty;

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
        _title = title;

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

    // Metoda do zmiany tytułu istniejącego powiadomienia
    public void SetTitle(string title)
    {
        _title = title;

        if (_notificationManager == null)
        {
            _notificationManager = (NotificationManager)MainActivity.ActivityCurrent.GetSystemService(Context.NotificationService);
        }

        // Tworzymy nowe powiadomienie z nowym tytułem
        _notification = new Notification.Builder(MainActivity.ActivityCurrent, "ServiceChannel")
            .SetContentTitle(title)
            //.SetContentText("Service is still running")
            .SetSmallIcon(Resource.Drawable.abc_ab_share_pack_mtrl_alpha)
            .SetOngoing(true)
            .SetContentIntent(GetPendingIntent()) // Kliknięcie powiadomienia uruchamia aplikację
            .Build();

        // Zaktualizuj istniejące powiadomienie
        _notificationManager.Notify(_notificationId, _notification);
    }

    private void RegisterNotification()
    {
        // Tworzymy kanał powiadomień
        NotificationChannel channel = new NotificationChannel("ServiceChannel", "ServiceDemo", NotificationImportance.Default);
        _notificationManager = (NotificationManager)MainActivity.ActivityCurrent.GetSystemService(Context.NotificationService);
        _notificationManager.CreateNotificationChannel(channel);

        // Tworzymy powiadomienie
        _notification = new Notification.Builder(this, "ServiceChannel")
            .SetContentTitle("Service Working")
            //.SetContentText("Service is still running")
            .SetSmallIcon(Resource.Drawable.abc_ab_share_pack_mtrl_alpha)
            .SetOngoing(true)
            .SetContentIntent(GetPendingIntent()) // Kliknięcie powiadomienia uruchamia aplikację
            .Build();

        // Uruchamiamy powiadomienie w tle
        StartForeground(_notificationId, _notification);
    }

    // Tworzymy PendingIntent, który uruchomi aplikację po kliknięciu powiadomienia
    private PendingIntent GetPendingIntent()
    {
        var intent = new Intent(this, typeof(MainActivity)); // Główna aktywność aplikacji
        return PendingIntent.GetActivity(MainActivity.ActivityCurrent, 0, intent, PendingIntentFlags.UpdateCurrent);
    }
}
