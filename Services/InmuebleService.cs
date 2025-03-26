using ComShopApp.Models.Backend.Inmueble;
using ComShopApp.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ComShopApp.Services;

public class InmuebleService
{
    private readonly HttpClient _httpClient;

    private Settings _settings;

    public InmuebleService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _settings = configuration.GetRequiredSection(nameof(Settings)).Get<Settings>();
    }

    public async Task<List<CategoryResponse>> GetCategories()
    {
        var uri = $"{_settings.BaseUrl}/api/category";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));

        var result = await _httpClient.GetStringAsync(uri);

        return JsonConvert.DeserializeObject<List<CategoryResponse>>(result);
    }

    public async Task<List<InmuebleResponse>> GetInmueblesByCategory(int categoryId)
    {
        var uri = $"{_settings.BaseUrl}/api/inmueble/category/{categoryId}";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));

        var result = await _httpClient.GetStringAsync(uri);

        return JsonConvert.DeserializeObject<List<InmuebleResponse>>(result);
    }

    public async Task<List<InmuebleResponse>> GetTrendingInmuebles()
    {
        var uri = $"{_settings.BaseUrl}/api/inmueble/trending";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));

        var result = await _httpClient.GetStringAsync(uri);

        return JsonConvert.DeserializeObject<List<InmuebleResponse>>(result);
    }

    public async Task<InmuebleResponse> GetInmuebleById(int inmuebleId)
    {
        var uri = $"{_settings.BaseUrl}/api/inmueble/{inmuebleId}";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));

        var result = await _httpClient.GetStringAsync(uri);

        return JsonConvert.DeserializeObject<InmuebleResponse>(result);
    }

    public async Task<bool> SaveBookmark(BookmarkRequest bookmark)
    {
        var uri = $"{_settings.BaseUrl}/api/bookmark";
        var json = JsonConvert.SerializeObject(bookmark);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));

        var result = await _httpClient.PostAsync(uri, content);

        if (result.IsSuccessStatusCode) return false;

        return true;
    }

    public async Task<List<InmuebleResponse>> GetBookmarks()
    {
        var uri = $"{_settings.BaseUrl}/api/inmueble/bookmark";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));

        var result = await _httpClient.GetStringAsync(uri);

        return JsonConvert.DeserializeObject<List<InmuebleResponse>>(result);
    }
    
    public async Task<List<InmuebleResponse>> GetSearchInmuebles(string searchText)
    {
        var uri = $"{_settings.BaseUrl}/api/inmueble/search/{searchText}";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));

        var result = await _httpClient.GetStringAsync(uri);

        return JsonConvert.DeserializeObject<List<InmuebleResponse>>(result);
    }
}
