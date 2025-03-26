using ComShopApp.ViewModels;

namespace ComShopApp.Views;

public partial class InmuebleDetailPage : ContentPage
{
	public InmuebleDetailPage(InmuebleDetailViewModel inmuebleDetailViewModel)
	{
		InitializeComponent();
		BindingContext = inmuebleDetailViewModel;
	}
}