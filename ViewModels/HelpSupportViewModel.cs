using CommunityToolkit.Mvvm.ComponentModel;
using ComShopApp.DataAccess;
using ComShopApp.Interfaces;
using ComShopApp.Utils;
using ComShopApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ComShopApp.ViewModels;

public partial class HelpSupportViewModel : ViewModelGlobal
{
    [ObservableProperty]
    public int pendingVisitors;

    [ObservableProperty]
    private ObservableCollection<Client> clients;

    [ObservableProperty]
    private Client selectedClient;

    private readonly INavigationService _navigation;

    public HelpSupportViewModel(INavigationService navigationService)
    {
        // asi se hace un enlace de datos con un dbCOntext
        var dbContext = new ShopDBContext();
        Clients = new ObservableCollection<Client>(dbContext.Clients);

        PropertyChanged += HelpSupportData_PropertyChanged;
        // para hacer esto que es inyeccion de dependencias, se necesita crear la instancia en MauiProgram.cs(entry point)
        // donde puedes indicar 
        _navigation = navigationService;
    }

    private async void HelpSupportData_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(SelectedClient))
        {
            var uri = $"{nameof(HelpSupportDetailPage)}?id={SelectedClient.Id}";
            // esto no se recomienda porque es una dependencia muy fuerte con el view model
            // que deberia de ser solo para pasar datos del modelo a la vista y viceversa
            // las operaciones directamente con el Shell deberian intentar evitarse
            //await Shell.Current.GoToAsync(uri);
            // ahora creamos una interface y un service de navegacion
            await _navigation.GoToAsync(uri);
        }
    }

}
