using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiGame.ViewModels;
public partial class TestViewModel : ObservableObject
{
    [ObservableProperty]
    private string _test = "test";
}
