using Android.App;
using AndroidX.Core.App;
using MauiGame.Services;

namespace MauiGame.Platforms.Android.Services;

public class NotificationService : INotificationService
{
    public void Show(string title, string message)
    {
        if (BackgroundService.ServiceNotificationChannel == null) return;

        var compatManager = NotificationManagerCompat.From(Platform.AppContext);

        var notification = new Notification.Builder(Platform.AppContext, BackgroundService.ServiceNotificationChannel.Id)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.abc_ab_share_pack_mtrl_alpha)
                .SetOngoing(true)
                .SetContentIntent(BackgroundService.GetPendingIntent())
                .Build();

        compatManager.Notify(BackgroundService.ServiceNotificationId, notification);
    }

    /*
    const string channelId = "default";
    const string channelName = "Default";
    const string channelDescription = "The default channel for notifications.";

    public const string TitleKey = "title";
    public const string MessageKey = "message";

    bool channelInitialized = false;
    internal int MessageId { get; } = 0;
    int pendingIntentId = 0;

    NotificationManagerCompat compatManager;

    public event EventHandler NotificationReceived;

    public static NotificationService Instance { get; private set; }

    public NotificationService()
    {
        if (Instance == null)
        {
            CreateNotificationChannel();
            compatManager = NotificationManagerCompat.From(Platform.AppContext);
            Instance = this;
        }
    }

    public void SendNotification(string title, string message, DateTime? notifyTime = null)
    {
        if (!channelInitialized)
        {
            CreateNotificationChannel();
        }

        if (notifyTime != null)
        {
            //Intent intent = new Intent(Platform.AppContext, typeof(AlarmHandler));
            //intent.PutExtra(TitleKey, title);
            //intent.PutExtra(MessageKey, message);
            //intent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);

            //var pendingIntentFlags = (Build.VERSION.SdkInt >= BuildVersionCodes.S)
            //    ? PendingIntentFlags.CancelCurrent | PendingIntentFlags.Immutable
            //    : PendingIntentFlags.CancelCurrent;

            //PendingIntent pendingIntent = PendingIntent.GetBroadcast(Platform.AppContext, pendingIntentId++, intent, pendingIntentFlags);
            //long triggerTime = GetNotifyTime(notifyTime.Value);
            //AlarmManager alarmManager = Platform.AppContext.GetSystemService(Context.AlarmService) as AlarmManager;
            //alarmManager.Set(AlarmType.RtcWakeup, triggerTime, pendingIntent);
        }
        else
        {
            Show(title, message);
        }
    }

    public void ReceiveNotification(string title, string message)
    {
        //var args = new NotificationEventArgs()
        //{
        //    Title = title,
        //    Message = message,
        //};
        //NotificationReceived?.Invoke(null, args);
    }

    public void Show(string title, string message)
    {
        //Intent intent = new Intent(Platform.AppContext, typeof(MainActivity));
        //intent.PutExtra(TitleKey, title);
        //intent.PutExtra(MessageKey, message);
        //intent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);

        //var pendingIntentFlags = (Build.VERSION.SdkInt >= BuildVersionCodes.S)
        //    ? PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable
        //    : PendingIntentFlags.UpdateCurrent;

        //PendingIntent pendingIntent = PendingIntent.GetActivity(Platform.AppContext, pendingIntentId++, intent, pendingIntentFlags);

        //NotificationCompat.Builder builder = new NotificationCompat.Builder(Platform.AppContext, channelId)
        //    .SetContentIntent(pendingIntent)
        //    .SetContentTitle(title)
        //    .SetContentText(message)
        //    .SetOngoing(true)
        //    //.SetLargeIcon(BitmapFactory.DecodeResource(Platform.AppContext.Resources, Resource.Drawable.dotnet_logo))
        //    //.SetSmallIcon(Resource.Drawable.message_small);
        //    .SetSmallIcon(Resource.Drawable.abc_ab_share_pack_mtrl_alpha);

        //Notification notification = builder.Build();
        //compatManager.Notify(messageId++, notification);

        var notification = GetNotification(title, message, Platform.AppContext);

        compatManager.Notify(MessageId, notification);
    }

    internal Notification GetNotification(string title, string message, Context context)
    {
        if (!channelInitialized)
        {
            CreateNotificationChannel();
        }

        Intent intent = new Intent(Platform.AppContext, typeof(MainActivity));
        intent.PutExtra(TitleKey, title);
        intent.PutExtra(MessageKey, message);
        intent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);

        var pendingIntentFlags = (Build.VERSION.SdkInt >= BuildVersionCodes.S)
            ? PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable
            : PendingIntentFlags.UpdateCurrent;

        PendingIntent pendingIntent = PendingIntent.GetActivity(Platform.AppContext, pendingIntentId++, intent, pendingIntentFlags);

        NotificationCompat.Builder builder = new NotificationCompat.Builder(context, channelId)
            .SetContentIntent(pendingIntent)
            .SetContentTitle(title)
            .SetContentText(message)
            .SetOngoing(true)
            //.SetLargeIcon(BitmapFactory.DecodeResource(Platform.AppContext.Resources, Resource.Drawable.dotnet_logo))
            //.SetSmallIcon(Resource.Drawable.message_small);
            .SetSmallIcon(Resource.Drawable.abc_ab_share_pack_mtrl_alpha);

        Notification notification = builder.Build();

        return notification;
    }

    private void CreateNotificationChannel()
    {
        // Create the notification channel, but only on API 26+.
        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            var channelNameJava = new Java.Lang.String(channelName);
            var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Default)
            {
                Description = channelDescription
            };
            // Register the channel
            NotificationManager manager = (NotificationManager)Platform.AppContext.GetSystemService(Context.NotificationService);
            manager.CreateNotificationChannel(channel);
            channelInitialized = true;
        }
    }

    private long GetNotifyTime(DateTime notifyTime)
    {
        DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
        double epochDiff = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
        long utcAlarmTime = utcTime.AddSeconds(-epochDiff).Ticks / 10000;
        return utcAlarmTime; // milliseconds
    }
    */
}
