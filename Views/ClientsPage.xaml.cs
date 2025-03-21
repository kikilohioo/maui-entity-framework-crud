using ComShopApp.ViewModels;

namespace ComShopApp.Views;

public partial class ClientsPage : ContentPage
{
	public ClientsPage(ClientsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}