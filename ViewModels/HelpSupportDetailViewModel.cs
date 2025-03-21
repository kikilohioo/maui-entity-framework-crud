using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComShopApp.DataAccess;
using ComShopApp.Services;
using ComShopApp.Utils;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ComShopApp.ViewModels;

public partial class HelpSupportDetailViewModel : ViewModelGlobal, IQueryAttributable
{
    private readonly IConnectivity _connectivity;

    private PurchaseService _purchaseService;

    private readonly ShopOutDBContext _shopOutDBContext;

    [ObservableProperty]
    private ObservableCollection<Purchase> purchases = new ObservableCollection<Purchase>();

    [ObservableProperty]
    private int clientId;

    [ObservableProperty]
    private ObservableCollection<Product> products;

    [ObservableProperty]
    private Product selectedProduct;

    [ObservableProperty]
    private int quantity;

    public HelpSupportDetailViewModel(
        IConnectivity connectivity,
        PurchaseService purchaseService,
        ShopOutDBContext shopOutDBContext
        )
    {
        var dbContext = new ShopDBContext();
        Products = new ObservableCollection<Product>(dbContext.Products);
        Quantity = 1;
        AddCommand = new Command(() =>
        {
            if (SelectedProduct is null)
            {
                return;
            }
            var purchase = new Purchase(
                ClientId,
                SelectedProduct.Id,
                Quantity,
                SelectedProduct.Name,
                SelectedProduct.Price,
                SelectedProduct.Price * Quantity
                );
            Purchases.Add(purchase);
        },
        // acá podria ir toda una logia para saber si se puede o no se puede ejecutar este comando
        () => true
        );
        _connectivity = connectivity;
        _connectivity.ConnectivityChanged += _connectivity_ConnectivityChanged;
        _purchaseService = purchaseService;
        _shopOutDBContext = shopOutDBContext;
    }

    [RelayCommand(CanExecute = nameof(StatusConnection))]
    private async void SendPurchases()
    {
        _shopOutDBContext.Database.EnsureCreated();

        foreach (var item in Purchases)
        {
            _shopOutDBContext.Purchases.Add(new PurchaseItem(
                item.ClientId,
                item.ProductId,
                item.Quantity,
                item.ProductPrice
                ));
        }

        await _shopOutDBContext.SaveChangesAsync();
        await Shell.Current.DisplayAlert("Mensaje", "Compras guardadas en base de datos local", "Aceptar");
    }

    private void _connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
    {
        SendPurchasesCommand.NotifyCanExecuteChanged();
    }

    private bool StatusConnection()
    {
        return _connectivity.NetworkAccess == NetworkAccess.Internet;
    }

    public ICommand AddCommand { get; set; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var clientId = int.Parse(query["id"].ToString());
        ClientId = clientId;
    }

    [RelayCommand]
    private void DeletePurchase(Purchase purchase)
    {
        Purchases.Remove(purchase);
    }
}
