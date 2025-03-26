using CommunityToolkit.Maui;
using ComShopApp.DataAccess;
using ComShopApp.Interfaces;
using ComShopApp.Services;
using ComShopApp.ViewModels;
using ComShopApp.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace ComShopApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            // asi cargamos las configuraciones a nuestra clase Settings
            var assemblyInstance = Assembly.GetExecutingAssembly();
            using var stream = assemblyInstance.GetManifestResourceStream("ComShopApp.appsettings.json");

            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Configuration.AddConfiguration(config);

            /* ##### SINGLETON ##### */
            // registramos servicios globales como AddSingleton, asi una sola vez se inicializa y se utiliza en todo el codigo
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton(Connectivity.Current);
            builder.Services.AddSingleton<PurchaseService>();
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<InmuebleService>();

            // login service, page y vm
            builder.Services.AddSingleton<SecurityService>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();

            // database service singleton
            builder.Services.AddSingleton<IDBPathService, DBPathService>();
            builder.Services.AddDbContext<ShopOutDBContext>();

            /* ##### TRANSIENT ##### */
            // aqui se agregan creaciones de instancias nuevas bajo demanda,
            // cada que una parte del codigo necesita que se inyecte alguna de estas dependencias registradas
            // como AddTransient, se crearan nuevas instancias de las mismas
            builder.Services.AddTransient<HelpSupportDetailViewModel>();
            builder.Services.AddTransient<HelpSupportDetailPage>();

            builder.Services.AddTransient<HelpSupportViewModel>();
            builder.Services.AddTransient<HelpSupportPage>();

            builder.Services.AddTransient<ClientsViewModel>();
            builder.Services.AddTransient<ClientsPage>();

            builder.Services.AddTransient<ProductDetailsViewModel>();
            builder.Services.AddTransient<ProductDetailPage>();

            builder.Services.AddTransient<ProductsViewModel>();
            builder.Services.AddTransient<ProductsPage>();

            builder.Services.AddTransient<DashboardViewModel>();
            builder.Services.AddTransient<DashboardPage>();

            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<HomePage>();

            builder.Services.AddTransient<BookmarkViewModel>();
            builder.Services.AddTransient<BookmarkPage>();

            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<SettingsPage>();

            builder.Services.AddTransient<InmuebleListViewModel>();
            builder.Services.AddTransient<InmuebleListPage>();

            builder.Services.AddTransient<InmuebleDetailViewModel>();
            builder.Services.AddTransient<InmuebleDetailPage>();

            builder.Services.AddTransient<InmuebleSearchViewModel>();
            builder.Services.AddTransient<InmuebleSearchPage>();

            /* ##### SCOPED ##### */

            var dbContext = new ShopDBContext();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();

            // registrar esto de esta forma es necesario para las rutas que necesitan
            // recibir parametros como el id de un producto para mostrar detalles, etc
            Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));
            Routing.RegisterRoute(nameof(HelpSupportDetailPage), typeof(HelpSupportDetailPage));
            Routing.RegisterRoute(nameof(InmuebleListPage), typeof(InmuebleListPage));
            Routing.RegisterRoute(nameof(InmuebleDetailPage), typeof(InmuebleDetailPage));
            Routing.RegisterRoute(nameof(InmuebleSearchPage), typeof(InmuebleSearchPage));

            // estos ultimos dos se registran para hacer bien la logica del login y logout
            Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
