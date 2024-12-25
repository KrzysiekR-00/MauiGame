using MauiGame.ViewModels;

namespace MauiGame.Services
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly INavigationService _navigationService;
        //private readonly IViewModelFactory _viewModelFactory;
        private readonly IPedometerService _pedometerService;
        private readonly IBackgroundService _backgroundService;
        private readonly INotificationService _notificationService;

        public ViewModelFactory(
            INavigationService navigationService,
            //IViewModelFactory viewModelFactory,
            IPedometerService pedometerService,
            IBackgroundService backgroundService,
            INotificationService notificationService)
        {
            _navigationService = navigationService;
            //_viewModelFactory = viewModelFactory;
            _pedometerService = pedometerService;
            _backgroundService = backgroundService;
            _notificationService = notificationService;
        }

        public MainViewModel CreateMainViewModel()
        {
            return new MainViewModel(_navigationService, this, _pedometerService, _backgroundService, _notificationService);
        }

        public TestViewModel CreateTestViewModel(string test)
        {
            return new TestViewModel(_navigationService, this, test);
        }
    }
}
