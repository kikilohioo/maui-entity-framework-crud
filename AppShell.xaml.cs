using ComShopApp.Views;

namespace ComShopApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void IrAExampleDotCom_Clicked(object sender, EventArgs e)
        {
            var uri = new Uri("https://example.com");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
    }
}
