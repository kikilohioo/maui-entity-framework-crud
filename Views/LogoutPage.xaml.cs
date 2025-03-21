namespace ComShopApp.Views;

public partial class LogoutPage : ContentPage
{
	public LogoutPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
        Shell.Current.GoToAsync($"{nameof(LoginPage)}");
    }
}