using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiGame.Services;
public interface INavigationService
{
    Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null);

    Task NavigateTo(ObservableObject observableObject);
}
