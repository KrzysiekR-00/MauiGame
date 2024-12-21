using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiGame.Services;
internal class NavigationService : INavigationService
{
    public Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null)
    {
        return
            routeParameters != null
                ? Shell.Current.GoToAsync(route, routeParameters)
                : Shell.Current.GoToAsync(route);
    }

    public async Task NavigateTo(ObservableObject observableObject)
    {
        var route = "//" + observableObject.GetType().Name.Replace("ViewModel", "View");

        await Shell.Current.GoToAsync(route);

        var page = (Shell.Current?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage;

        if (page != null)
        {
            page.BindingContext = observableObject;
        }
    }
}
