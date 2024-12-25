using CommunityToolkit.Mvvm.ComponentModel;
using MauiGame.Services;

namespace MauiGame.ViewModels
{
    public abstract class ViewModel : ObservableObject
    {
        private protected INavigationService NavigationService { get; }
        private protected IViewModelFactory ViewModelFactory { get; }

        public ViewModel(INavigationService navigationService, IViewModelFactory viewModelFactory)
        {
            NavigationService = navigationService;
            ViewModelFactory = viewModelFactory;
        }
    }
}
