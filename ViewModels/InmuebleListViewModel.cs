using CommunityToolkit.Mvvm.ComponentModel;
using ComShopApp.Interfaces;
using ComShopApp.Models.Backend.Inmueble;
using ComShopApp.Services;
using ComShopApp.Utils;
using ComShopApp.Views;
using System.Collections.ObjectModel;

namespace ComShopApp.ViewModels;

public partial class InmuebleListViewModel : ViewModelGlobal, IQueryAttributable
{
    [ObservableProperty]
    InmuebleResponse selectedInmueble;

    private readonly INavigationService _navigationService;

    [ObservableProperty]
    ObservableCollection<InmuebleResponse> inmuebles;

    private readonly InmuebleService _inmuebleService;

    public InmuebleListViewModel(INavigationService navigationService, InmuebleService inmuebleService)
    {
        _navigationService = navigationService;
        _inmuebleService = inmuebleService;
        PropertyChanged += InmuebleListViewModel_PropertyChanged;
    }

    private async void InmuebleListViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(SelectedInmueble))
        {
            var uri = $"{nameof(InmuebleDetailPage)}?id={SelectedInmueble.Id}";
            await _navigationService.GoToAsync(uri);
        }
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var id = int.Parse(query["id"].ToString());
        await LoadData(categoryId: id);
    }

    public async Task LoadData(int categoryId)
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            var inmueblesList = await _inmuebleService.GetInmueblesByCategory(categoryId);
            Inmuebles = new ObservableCollection<InmuebleResponse>(inmueblesList);
        }catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
        }
        finally
        {
            IsBusy = false;
        }
    }


}
