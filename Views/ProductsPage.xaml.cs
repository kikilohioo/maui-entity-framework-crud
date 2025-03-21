using ComShopApp.DataAccess;
using ComShopApp.ViewModels;
using System.ComponentModel;

namespace ComShopApp.Views;

public partial class ProductsPage : ContentPage
{
	public ProductsPage(ProductsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}