using ComShopApp.Services;

namespace ComShopApp.Views;

public partial class LoadingPage : ContentPage
{
	private readonly SecurityService _securityService;

	public LoadingPage(SecurityService securityService)
	{
		InitializeComponent();
		_securityService = securityService;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
		bool isLogged = await _securityService.AuthenticationState();

        if (isLogged)
		{
            await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
        }
		else
		{
			await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
		}
    }
}