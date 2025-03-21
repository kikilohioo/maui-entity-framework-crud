using ComShopApp.ViewModels;

namespace ComShopApp.Views;

public partial class HelpSupportPage : ContentPage
{
	public HelpSupportPage(HelpSupportViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void UpdateButton_Clicked(object sender, EventArgs e)
    {
		// asi capturamos el enlace de los resources creados en el .xaml y modificamos a gusto el Source
		// para hacer que la clase ClaseData notifique hay que hacer que implemente al interface INotifyPropertyChanged
		var dataObject = Resources["data"] as HelpSupportViewModel;
		dataObject.PendingVisitors = 30;
    }
}