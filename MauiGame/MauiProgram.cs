using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Pedometer;

namespace MauiGame
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<ViewModels.MainViewModel>();
            //builder.Services.AddSingleton<TestViewModel>();

            builder.Services.AddTransient<Services.INavigationService, Services.NavigationService>();
            builder.Services.AddTransient<Services.IViewModelFactory, Services.ViewModelFactory>();

#if ANDROID
            builder.Services.AddSingleton(Pedometer.Default);

            builder.Services.AddTransient<Services.IBackgroundService, Platforms.Android.Services.BackgroundService>();
            builder.Services.AddTransient<Services.INotificationService, Platforms.Android.Services.NotificationService>();
            builder.Services.AddTransient<Services.IPedometerService, Platforms.Android.Services.PedometerService>();
#endif

#if WINDOWS
            builder.Services.AddTransient<Services.IBackgroundService, Platforms.Windows.Services.DummyBackgroundService>();
            builder.Services.AddTransient<Services.INotificationService, Platforms.Windows.Services.DummyNotificationService>();
            builder.Services.AddTransient<Services.IPedometerService, Platforms.Windows.Services.DummyPedometerService>();
#endif

            return builder.Build();
        }
    }
}
