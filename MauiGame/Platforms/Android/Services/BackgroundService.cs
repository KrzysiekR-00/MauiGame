using Android.App;
using Android.Content;
using Android.OS;
using MauiGame.Services;

namespace MauiGame.Platforms.Android.Services;

[Service]
public class BackgroundService : Service, IBackgroundService
{
    internal const int ServiceNotificationId = 1;

    internal static NotificationChannel? ServiceNotificationChannel { get; private set; } = null;

    public override StartCommandResult OnStartCommand(Intent? intent, StartCommandFlags flags, int startId)
    {
        ServiceNotificationChannel = new NotificationChannel(
            "ServiceChannel",
            "ServiceDemo",
            NotificationImportance.Default);

        var manager = (NotificationManager?)Platform.AppContext.GetSystemService(NotificationService);

        if (manager != null)
        {
            manager.CreateNotificationChannel(ServiceNotificationChannel);

            var notification = new Notification.Builder(this, ServiceNotificationChannel.Id)
                .SetContentTitle("Service Working")
                .SetContentText("Service Working")
                .SetSmallIcon(Resource.Drawable.abc_ab_share_pack_mtrl_alpha)
                .SetOngoing(true)
                .SetContentIntent(GetPendingIntent())
                .Build();

            StartForeground(ServiceNotificationId, notification);
        }

        return StartCommandResult.NotSticky;
    }

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

    internal static PendingIntent? GetPendingIntent()
    {
        var intent = new Intent(Platform.AppContext, typeof(MainActivity));
        intent.AddFlags(ActivityFlags.NewTask);
        intent.PutExtra("startMainPage", true);
        return PendingIntent.GetActivity(
            Platform.AppContext,
            0,
            intent,
            PendingIntentFlags.UpdateCurrent);
    }



    /*

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

            //MauiGame.Platforms.Android.Services.NotificationService.Instance.SendNotification("Title", "Message");
        }
        else if (intent.Action == "STOP_SERVICE")
        {
            StopForeground(true);
            StopSelfResult(startId);
        }

        return StartCommandResult.NotSticky;
    }

    public void Start()
    {
        //_title = title;

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

    private void RegisterNotification()
    {
        // Tworzymy kanał powiadomień
        //NotificationChannel channel = new NotificationChannel("ServiceChannel", "ServiceDemo", NotificationImportance.Default);
        //_notificationManager = (NotificationManager)MainActivity.ActivityCurrent.GetSystemService(Context.NotificationService);
        //_notificationManager.CreateNotificationChannel(channel);

        //// Tworzymy powiadomienie
        //_notification = new Notification.Builder(this, "ServiceChannel")
        //    .SetContentTitle("Service Working")
        //    //.SetContentText("Service is still running")
        //    .SetSmallIcon(Resource.Drawable.abc_ab_share_pack_mtrl_alpha)
        //    .SetOngoing(true)
        //    .SetContentIntent(GetPendingIntent()) // Kliknięcie powiadomienia uruchamia aplikację
        //    .Build();

        //// Uruchamiamy powiadomienie w tle

        var notification = MauiGame.Platforms.Android.Services.NotificationService.Instance.GetNotification("Service Working", "Service Working", this);

        var id = MauiGame.Platforms.Android.Services.NotificationService.Instance.MessageId;

        StartForeground(id, notification);
    }

    /*
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
        // Stwórz Intent, który uruchomi główną stronę aplikacji MAUI (MainPage)
        var intent = new Intent(this, typeof(MainActivity));
        intent.AddFlags(ActivityFlags.NewTask); // Umożliwia uruchomienie nowej aktywności w nowym zadaniu

        // Otwórz stronę aplikacji MAUI, nie tylko aktywność
        intent.PutExtra("startMainPage", true); // Możesz dodać dane do Intent, jeśli chcesz, aby MainActivity wykonała specjalne akcje
        return PendingIntent.GetActivity(MainActivity.ActivityCurrent, 0, intent, PendingIntentFlags.UpdateCurrent);
    }
    */
}
