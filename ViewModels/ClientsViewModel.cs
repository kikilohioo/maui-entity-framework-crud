using CommunityToolkit.Mvvm.ComponentModel;
using ComShopApp.DataAccess;
using ComShopApp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComShopApp.ViewModels;

public partial class ClientsViewModel : ViewModelGlobal
{
    [ObservableProperty]
    ObservableCollection<Client> clients;

    [ObservableProperty]
    Client selectedClient;

    public ClientsViewModel()
    {
        var dbContext = new ShopDBContext();
        Clients  = new ObservableCollection<Client>(dbContext.Clients);
    }
}
