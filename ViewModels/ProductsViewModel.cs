using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComShopApp.DataAccess;
using ComShopApp.Interfaces;
using ComShopApp.Utils;
using ComShopApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ComShopApp.ViewModels;

public partial class ProductsViewModel : ViewModelGlobal
{
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    ObservableCollection<Product> products;

    [ObservableProperty]
    Product selectedProduct;

    [ObservableProperty]
    bool isRefreshing;

    public ProductsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        LoadProductList();
        PropertyChanged += ProductsViewModel_PropertyChanged; ;
    }

    [RelayCommand]
    private async Task Refresh()
    {
        LoadProductList();
        await Task.Delay(1500);
        IsRefreshing = false;
    }

    private async void ProductsViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(SelectedProduct))
        {
            var uri = $"{nameof(ProductDetailPage)}?id={SelectedProduct.Id}";
            await _navigationService.GoToAsync(uri);
        }
    }

    private void LoadProductList()
    {
        var dbContext = new ShopDBContext();
        Products = new ObservableCollection<Product>(dbContext.Products);
    }
}
