using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiGame.Services;

namespace MauiGame.ViewModels;
//[QueryProperty(nameof(Test), "TestParameter")]
public partial class TestViewModel : ViewModel
{
    //private readonly INavigationService _navigationService;

    [ObservableProperty]
    private string _test = "test";

    //public TestViewModel()
    //{
    //    _navigationService = new NavigationService();
    //}

    public TestViewModel(
        INavigationService navigationService,
        IViewModelFactory viewModelFactory,
        string test)
        : base(navigationService, viewModelFactory)
    {
        Test = test;

        //_navigationService = new NavigationService();
    }

    [RelayCommand]
    private void NavigationTest()
    {
        var mainViewModel = ViewModelFactory.CreateMainViewModel();

        NavigationService.NavigateTo(mainViewModel);
    }
}
