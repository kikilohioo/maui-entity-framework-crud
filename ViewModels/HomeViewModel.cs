using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComShopApp.Interfaces;
using ComShopApp.Models.Backend.Inmueble;
using ComShopApp.Services;
using ComShopApp.Utils;
using ComShopApp.Views;
using System.Collections.ObjectModel;

namespace ComShopApp.ViewModels;

public partial class HomeViewModel : ViewModelGlobal
{
    [ObservableProperty]
    string userName;

    [ObservableProperty]
    CategoryResponse selectedCategory;

    [ObservableProperty]
    ObservableCollection<CategoryResponse> categories;

    [ObservableProperty]
    ObservableCollection<InmuebleResponse> trendingInmuebles;

    public Command GetDataCommand { get; }

    private readonly InmuebleService _inmuebleService;

    private readonly INavigationService _navigationService;

    public HomeViewModel(InmuebleService inmuebleService, INavigationService navigationService)
    {
        _inmuebleService = inmuebleService;
        _navigationService = navigationService;
        UserName = Preferences.Get("user_name", string.Empty);
        // este es un comando configurado y ejecuta en el mismo viewmodel, viene bien para cargar datos
        // automaticamente y despues tener la posibilidad de buscar o lo que sea con otro boton que llame al mismo comando
        GetDataCommand = new Command(async() => await LoadDataAsync());
        GetDataCommand.Execute(this);
    }

    public async Task LoadDataAsync()
    {
        if(IsBusy) return;

        try
        {
            IsBusy = true;
            var categoriesList = await _inmuebleService.GetCategories();
            var trendingInmueblesList = await _inmuebleService.GetTrendingInmuebles();

            Categories = new ObservableCollection<CategoryResponse>(categoriesList);
            TrendingInmuebles = new ObservableCollection<InmuebleResponse>(trendingInmueblesList);
        }
        catch (HttpRequestException httpEx)
        {
            await Application.Current.MainPage.DisplayAlert("Error de conexión",
                "No se pudo conectar con el servidor. Verifica tu conexión a la red.", "Aceptar");
        }
        catch (Exception ex)
        {
            Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task CategorySelectedEvent()
    {
        var uri = $"{nameof(InmuebleListPage)}?id={SelectedCategory.Id}";
        await _navigationService.GoToAsync(uri);
    }

    [RelayCommand]
    async Task TapSearchInmuebles()
    {
        var uri = $"{nameof(InmuebleSearchPage)}";
        await _navigationService.GoToAsync(uri);
    }
}
