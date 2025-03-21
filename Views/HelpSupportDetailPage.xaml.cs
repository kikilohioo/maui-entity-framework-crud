
using ComShopApp.ViewModels;

namespace ComShopApp.Views;

public partial class HelpSupportDetailPage : ContentPage
{
	public HelpSupportDetailPage(HelpSupportDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}