using MauiGame.ViewModels;

namespace MauiGame.Services
{
    public interface IViewModelFactory
    {
        MainViewModel CreateMainViewModel();
        TestViewModel CreateTestViewModel(string test);
    }
}
