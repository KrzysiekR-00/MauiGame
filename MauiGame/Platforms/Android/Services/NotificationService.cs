using Android.App;
using Android.Content;
using AndroidX.Core.App;
using MauiGame.Services;

namespace MauiGame.Platforms.Android.Services;

public class NotificationService : INotificationService
{
    internal const int NotificationId = 1;

    private static NotificationChannel? _notificationChannel = null;

    public void Show(string title, string message)
    {
        if (GetNotificationChannelId() == null) return;

        var compatManager = NotificationManagerCompat.From(Platform.AppContext);

        var notification = CreateNotification(title, message);

        compatManager.Notify(NotificationId, notification);
    }

    internal static Notification CreateNotification(string title, string message)
    {
        return new Notification.Builder(Platform.AppContext, GetNotificationChannelId())
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.abc_ab_share_pack_mtrl_alpha)
                .SetOngoing(true)
                .SetContentIntent(GetPendingIntent())
                .Build();
    }

    internal static string? GetNotificationChannelId()
    {
        if (_notificationChannel == null)
        {
            _notificationChannel = new NotificationChannel(
                "ServiceChannel",
                "ServiceDemo",
                NotificationImportance.Default);

            var manager = (NotificationManager?)Platform.AppContext.GetSystemService(BackgroundService.NotificationService);

            if (manager != null)
            {
                manager.CreateNotificationChannel(_notificationChannel);
            }
        }

        return _notificationChannel.Id;
    }

    private static PendingIntent? GetPendingIntent()
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
}
