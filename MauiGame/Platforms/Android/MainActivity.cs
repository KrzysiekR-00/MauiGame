using Android.App;
using Android.Content.PM;
using Android.OS;

namespace MauiGame
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        public static MainActivity ActivityCurrent { get; set; }
        public MainActivity()
        {
            ActivityCurrent = this;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Jeśli został przekazany parametr "startMainPage", otwieramy MainPage
            var startMainPage = Intent.GetBooleanExtra("startMainPage", false);
            if (startMainPage)
            {
                // Tutaj przejdź do MainPage
                Shell.Current.GoToAsync("//Pages/MainPage");
            }
        }
    }
}
