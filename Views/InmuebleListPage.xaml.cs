using ComShopApp.ViewModels;

namespace ComShopApp.Views;

public partial class InmuebleListPage : ContentPage
{
	public InmuebleListPage(InmuebleListViewModel inmuebleListViewModel)
	{
		InitializeComponent();
		BindingContext = inmuebleListViewModel;
    }
}