using ComShopApp.ViewModels;

namespace ComShopApp.Views;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardViewModel dashboardViewModel)
	{
		InitializeComponent();
		BindingContext = dashboardViewModel;
	}
}