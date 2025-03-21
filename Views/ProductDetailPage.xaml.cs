
using ComShopApp.DataAccess;
using ComShopApp.ViewModels;

namespace ComShopApp.Views;

public partial class ProductDetailPage : ContentPage
{
	public ProductDetailPage(ProductDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}