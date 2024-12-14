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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton(Pedometer.Default);

#if ANDROID
            builder.Services.AddTransient<Services.IBackgroundService, Platforms.Android.Services.BackgroundService>();
#endif

            return builder.Build();
        }
    }
}
