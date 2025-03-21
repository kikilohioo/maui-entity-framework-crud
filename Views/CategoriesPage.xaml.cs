using ComShopApp.DataAccess;

namespace ComShopApp.Views;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage()
	{
		InitializeComponent();

        var dbContext = new ShopDBContext();

        foreach (var category in dbContext.Categories)
        {
            var button = new Button { Text = category.Name };

            button.Clicked += async (s, a) =>
            {
                var uri = $"{nameof(ProductDetailPage)}?id={category.Id}";
                await Shell.Current.GoToAsync(uri);
            };

            container.Children.Add(button);
        }
    }
}