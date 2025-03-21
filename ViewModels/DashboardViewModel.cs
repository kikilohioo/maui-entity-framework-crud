using CommunityToolkit.Mvvm.ComponentModel;
using ComShopApp.DataAccess;
using ComShopApp.Services;
using ComShopApp.Utils;
using ComShopApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComShopApp.ViewModels;

public partial class DashboardViewModel : ViewModelGlobal
{
    [ObservableProperty]
    int visitors;

    [ObservableProperty]
    int clients;

    [ObservableProperty]
    decimal total;

    [ObservableProperty]
    int productsTotal;

    public DashboardViewModel(ShopOutDBContext shopOutDBContext)
    {
        var db = new ShopDBContext();
        Visitors = shopOutDBContext.Purchases
            .ToList()
            .DistinctBy(p => p.ClientId)
            .Count();

        Clients = db.Clients.Count();
        Total = shopOutDBContext.Purchases.Sum(p => p.Price * p.Quantity);
        ProductsTotal = shopOutDBContext.Purchases.Sum(p => p.Quantity);
    }
}
