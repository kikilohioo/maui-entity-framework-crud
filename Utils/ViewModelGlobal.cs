using CommunityToolkit.Mvvm.ComponentModel;

namespace ComShopApp.Utils
{
    // esta clase nos ayuda a centralizar la gestion de las notificaciones de las propiedades entre
    // la Source y las views, ahora implementando esta clase personalizada podremos agregar esta logica
    // de forma mas limpia y sencilla
    // #########    VERSION CASER   #########
    //public class NotifyPropertyChanged : INotifyPropertyChanged
    //{
    //    public void RaisePropertyChanged([CallerMemberName]string propertyName = null)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }
    //    public event PropertyChangedEventHandler? PropertyChanged;
    //}
    // #########    VERSION CON TOOLKIT   #########
    public partial class ViewModelGlobal : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        public bool IsNotBusy => !IsBusy;
    }
}
