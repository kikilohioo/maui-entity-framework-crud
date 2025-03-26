using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComShopApp.Interfaces;
using ComShopApp.Models.Backend.Inmueble;
using ComShopApp.Services;
using ComShopApp.Utils;

namespace ComShopApp.ViewModels;

public partial class InmuebleDetailViewModel : ViewModelGlobal, IQueryAttributable
{
    [ObservableProperty]
    private string bookmarkImageSource;

    [ObservableProperty]
    private InmuebleResponse inmueble;

    private readonly InmuebleService _inmuebleService;
    private readonly INavigationService _navigationService;

    public InmuebleDetailViewModel(InmuebleService inmuebleService, INavigationService navigationService)
    {
        _inmuebleService = inmuebleService;
        _navigationService = navigationService;
    }

    public async Task LoadData(int inmuebleId)
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            Inmueble = await _inmuebleService.GetInmuebleById(inmuebleId);
            BookmarkImageSource = Inmueble.IsBookmarkEnabled ? "bookmark_fill_icon" : "bookmark_icon";
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

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var id = int.Parse(query["id"].ToString());
        await LoadData(inmuebleId: id);
    }

    [RelayCommand]
    async Task GoBackEvent()
    {
        await _navigationService.GoToAsync("..");
    }

    [RelayCommand]
    async Task SaveBookmark()
    {
        var bookmark = new BookmarkRequest
        {
            InmuebleId = Inmueble.Id,
            UserId = Preferences.Get("user_id", string.Empty)
        };

        await _inmuebleService.SaveBookmark(bookmark);
        await LoadData(Inmueble.Id);
    }

    [RelayCommand]
    async Task CallInmuebleOwner()
    {
        var confirm = Application.Current.MainPage.DisplayAlert(
            "Llamar",
            $"Desea llamar a este numero: {Inmueble.Phone}",
            "Si",
            "No"
            );

        if(await confirm)
        {
            try
            {
                PhoneDialer.Default.Open(Inmueble.Phone);
            }
            catch(ArgumentNullException)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "El numero de teléfono no es valido",
                    "Ok"
                    );
            }
            catch (FeatureNotSupportedException ex)
            {
                var error = ex.Message;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Funcionalidad no soportada por el dispositivo",
                    "Ok"
                    );
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Error inesperado al intentar realizar la llamada",
                    "Ok"
                    );
            }
        }
    }

    [RelayCommand]
    async Task TextMessageOwner()
    {
        var message = new SmsMessage("Hola, porfavor enviame información sobre el inmueble", Inmueble.Phone);
        var confirm = Application.Current.MainPage.DisplayAlert(
            "Enviar mensaje",
            $"Desea enviar un mensaje de texto a este numero: {Inmueble.Phone}",
            "Si",
            "No"
            );

        if (await confirm)
        {
            try
            {
                await Sms.Default.ComposeAsync(message);
            }
            catch (ArgumentNullException)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "El numero de teléfono no es valido",
                    "Ok"
                    );
            }
            catch (FeatureNotSupportedException ex)
            {
                var error = ex.Message;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Funcionalidad no soportada por el dispositivo",
                    "Ok"
                    );
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Error inesperado al intentar enviar el mensaje de texto",
                    "Ok"
                    );
            }
        }
    }
}
