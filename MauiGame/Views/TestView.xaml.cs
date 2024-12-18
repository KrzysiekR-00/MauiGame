using MauiGame.ViewModels;

namespace MauiGame.Views;

public partial class TestView : ContentPage
{
    public TestView(TestViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}