using AndroidX.Core.App;
using MauiGame.Services;

namespace MauiGame.Platforms.Android.Services;

public class NotificationService : INotificationService
{
    public void Show(string title, string message)
    {
        if (NotificationUtilities.GetNotificationChannelId() == null) return;

        var compatManager = NotificationManagerCompat.From(Platform.AppContext);

        var notification = NotificationUtilities.CreateNotification(title, message);

        compatManager.Notify(NotificationUtilities.NotificationId, notification);
    }
}
