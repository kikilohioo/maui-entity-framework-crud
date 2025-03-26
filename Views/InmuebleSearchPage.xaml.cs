using ComShopApp.ViewModels;

namespace ComShopApp.Views;

public partial class InmuebleSearchPage : ContentPage
{
	public InmuebleSearchPage(InmuebleSearchViewModel inmuebleSearchViewModel)
	{
		InitializeComponent();
		BindingContext = inmuebleSearchViewModel;
	}
}