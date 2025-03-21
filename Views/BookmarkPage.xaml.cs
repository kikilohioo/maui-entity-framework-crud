using ComShopApp.ViewModels;

namespace ComShopApp.Views;

public partial class BookmarkPage : ContentPage
{
	public BookmarkPage(BookmarkViewModel bookmarkViewModel)
	{
		InitializeComponent();
		BindingContext = bookmarkViewModel;
	}
}