using ComShopApp.DataAccess;
using System.Net.Http.Json;
using ComShopApp.Utils;
using Microsoft.Extensions.Configuration;

namespace ComShopApp.Services;

public class PurchaseService
{
    private HttpClient _httpClient;

    private Settings _settings;

    public PurchaseService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _settings = config.GetRequiredSection(nameof(Settings)).Get<Settings>();
    }

    public async Task<bool> SendPurchases(IEnumerable<Purchase> purchases)
    {
        var uri = $"{_settings.BaseUrl}/api/compra";
        var body = new
        {
            data = purchases
        };

        var result = await _httpClient.PostAsJsonAsync(uri, body);

        return result.IsSuccessStatusCode;
    }
}
