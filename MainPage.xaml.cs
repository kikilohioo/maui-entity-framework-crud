using ComShopApp.DataAccess;
using Microsoft.Maui.Controls.Shapes;

namespace ComShopApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            var dbContext = new ShopDBContext();
            category.Text = dbContext.Categories.Count().ToString();
            product.Text = dbContext.Products.Count().ToString();
            client.Text = dbContext.Clients.Count().ToString();
        }

        // Para probar codigo de animaciones
        //private void Rectanlge_Tapped(object sender, TappedEventArgs e)
        //{
        //    (sender as Rectangle)?.ScaleTo(2);
        //    (sender as Rectangle)?.TranslateTo(200, 200);
        //}
    }

}
