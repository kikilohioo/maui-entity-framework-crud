using ComShopApp.ViewModels;

namespace ComShopApp.Views;

public partial class BookmarkPage : ContentPage
{
	private BookmarkViewModel _viewModel;

	public BookmarkPage(BookmarkViewModel bookmarkViewModel)
	{
		InitializeComponent();
		BindingContext = bookmarkViewModel;
		_viewModel = bookmarkViewModel;
	}

    protected override void OnAppearing()
    {
        _viewModel.GetInmuebleCommand.Execute(this);
    }
}