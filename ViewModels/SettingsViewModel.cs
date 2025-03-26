using CommunityToolkit.Mvvm.Input;
using ComShopApp.Interfaces;
using ComShopApp.Utils;
using ComShopApp.Views;

namespace ComShopApp.ViewModels;

public partial class SettingsViewModel : ViewModelGlobal
{
    private readonly INavigationService _navigationService;

    public SettingsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    async Task Logout()
    {
        Preferences.Set("access_token", string.Empty);
        var uri = $"//{nameof(LoadingPage)}";
        await _navigationService.GoToAsync(uri);
    }
}
