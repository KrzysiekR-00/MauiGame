﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiGame.Services;

namespace MauiGame.ViewModels;
public partial class TestViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private string _test = "test";

    public TestViewModel()
    {
        _navigationService = new NavigationService();
    }

    [RelayCommand]
    private void NavigationTest()
    {
        _navigationService.NavigateToAsync("//MainView");
    }
}
