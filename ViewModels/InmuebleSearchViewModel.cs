using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComShopApp.Interfaces;
using ComShopApp.Models.Backend.Inmueble;
using ComShopApp.Services;
using ComShopApp.Utils;
using ComShopApp.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ComShopApp.ViewModels;

public partial class InmuebleSearchViewModel : ViewModelGlobal
{
    [ObservableProperty]
    ObservableCollection<InmuebleResponse> inmuebles;

    [ObservableProperty]
    private InmuebleResponse selectedInmueble;

    [ObservableProperty]
    private string searchText;

    private readonly InmuebleService _inmuebleService;
    private readonly INavigationService _navigationService;

    public InmuebleSearchViewModel(InmuebleService inmuebleService, INavigationService navigationService)
    {
        _inmuebleService = inmuebleService;
        _navigationService = navigationService;
        PropertyChanged += InmuebleSearchViewModel_PropertyChanged;
    }

    private async void InmuebleSearchViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(SelectedInmueble))
        {
            var uri = $"{nameof(InmuebleDetailPage)}?id={SelectedInmueble.Id}";
            await _navigationService.GoToAsync(uri);
        }
    }

    [RelayCommand]
    async Task GoBackEvent()
    {
        await _navigationService.GoToAsync("..");
    }

    [RelayCommand]
    async Task SearchInmuebles()
    {
        var inmuebleList = await _inmuebleService.GetSearchInmuebles(SearchText);
        Inmuebles = new ObservableCollection<InmuebleResponse>(inmuebleList);
    }
}
