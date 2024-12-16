using Android.App;
using Android.Content.PM;
using Android.OS;

namespace MauiGame
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        public MainActivity()
        {

        }

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var startMainPage = Intent?.GetBooleanExtra("startMainPage", false);
            if (startMainPage == true)
            {
                Shell.Current.GoToAsync("//Pages/MainPage");
            }
        }
    }
}
