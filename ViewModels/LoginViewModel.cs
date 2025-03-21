using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComShopApp.Services;
using ComShopApp.Utils;
using ComShopApp.Views;

namespace ComShopApp.ViewModels;

public partial class LoginViewModel : ViewModelGlobal
{
    private readonly IConnectivity _connectivity;

    private readonly SecurityService _securityService;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    public LoginViewModel(IConnectivity connectivity, SecurityService securityService)
    {
        _connectivity = connectivity;
        _securityService = securityService;
        _connectivity.ConnectivityChanged += _connectivity_ConnectivityChanged;
    }

    private void _connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
    {
        LoginCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(StatusConnection))]
    private async Task Login()
    {
        var result = await _securityService.Login(Email, Password);
        if (result)
        {
            await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
        }
        else
        {
            await Shell.Current.DisplayAlert("Mensaje", "Usuario y/o contraseña iconrrectas", "Aceptar");
        }
    }

    private bool StatusConnection()
    {
        return _connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
