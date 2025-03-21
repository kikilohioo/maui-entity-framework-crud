using ComShopApp.DataAccess;
using ComShopApp.Views;

namespace ComShopApp
{
    public partial class App : Application
    {
        public App(ShopOutDBContext shopOutDBContext)
        {
            InitializeComponent();
            shopOutDBContext.Database.EnsureCreated();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}