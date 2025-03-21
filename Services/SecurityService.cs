using ComShopApp.Models;
using ComShopApp.Models.Backend.Login;
using ComShopApp.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
namespace ComShopApp.Services;

public class SecurityService
{
    // TODO: acá tenemos un video para mejorar el proceso de autenticación completo https://www.youtube.com/watch?v=97G-XkuENYE&t=1673s
    private HttpClient _httpClient;
    private Settings _settings;

    public SecurityService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _settings = config.GetRequiredSection(nameof(Settings)).Get<Settings>();
    }

    public async Task<bool> AuthenticationState()
    {
        await Task.Delay(2000);
        var accessToken = Preferences.Get("access_token", string.Empty);
        return !string.IsNullOrEmpty(accessToken);
    }

    public async Task<bool> Login(string email, string password)
    {
        var uri = $"{_settings.BaseUrl}/api/usuario/login";
        var loginRequest = new LoginRequest
        {
            Email = email,
            Password = password
        };
        var json = JsonConvert.SerializeObject(loginRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(uri, content);

        if(!response.IsSuccessStatusCode) return false;

        var jsonResult = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<UserResponse>(jsonResult);

        Preferences.Set("access_token", result.Token);
        Preferences.Set("user_id", result.Id);
        Preferences.Set("email", result.Email);
        Preferences.Set("complete_name", $"{result.Name} {result.Lastname}");
        Preferences.Set("phone", result.Phone);
        Preferences.Set("user_name", result.UserName);

        return true;
    }

    public void Logout()
    {
        Preferences.Remove("access_token");
        Preferences.Remove("user_id");
        Preferences.Remove("email");
        Preferences.Remove("complete_name");
        Preferences.Remove("phone");
        Preferences.Remove("user_name");
    }
}
