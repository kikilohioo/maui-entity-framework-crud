using CommunityToolkit.Mvvm.ComponentModel;
using ComShopApp.Interfaces;
using ComShopApp.Models.Backend.Inmueble;
using ComShopApp.Services;
using ComShopApp.Utils;
using System.Collections.ObjectModel;

namespace ComShopApp.ViewModels;

public partial class BookmarkViewModel : ViewModelGlobal
{
    [ObservableProperty]
    ObservableCollection<InmuebleResponse> inmuebles;

    [ObservableProperty]
    private InmuebleResponse selectedInmueble;

    private readonly INavigationService _navigationService;
    private readonly InmuebleService _inmuebleService;

    public BookmarkViewModel(INavigationService navigationService, InmuebleService inmuebleService)
    {
        _navigationService = navigationService;
        _inmuebleService = inmuebleService;
        GetInmuebleCommand = new Command(async () => await LoadData());
        PropertyChanged += BookmarkViewModel_PropertyChanged;
    }

    private async void BookmarkViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(SelectedInmueble))
        {
            var uri = $"{nameof(SelectedInmueble)}id={SelectedInmueble.Id}";
            await _navigationService.GoToAsync(uri);
        }
    }

    public Command GetInmuebleCommand { get; }

    public async Task LoadData()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            var inmueblesList = await _inmuebleService.GetBookmarks();
            Inmuebles = new ObservableCollection<InmuebleResponse>(inmueblesList);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
