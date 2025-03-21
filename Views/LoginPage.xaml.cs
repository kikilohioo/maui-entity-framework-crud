using ComShopApp.ViewModels;

namespace ComShopApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
		Preferences.Set("access_token", null);
		BindingContext = loginViewModel;
	}
}