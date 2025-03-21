using CommunityToolkit.Mvvm.ComponentModel;
using ComShopApp.DataAccess;
using ComShopApp.Utils;

namespace ComShopApp.ViewModels;

public partial class ProductDetailsViewModel : ViewModelGlobal, IQueryAttributable
{
    [ObservableProperty]
    string name;

    [ObservableProperty]
    string description;

    [ObservableProperty]
    decimal price;

    public ProductDetailsViewModel()
    {

    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var dbContext = new ShopDBContext();
        var id = int.Parse(query["id"].ToString());
        var product = dbContext.Products.FirstOrDefault(p => p.Id == id);

        Name = product.Name;
        Description = product.Description;
        Price = product.Price;
    }
}
