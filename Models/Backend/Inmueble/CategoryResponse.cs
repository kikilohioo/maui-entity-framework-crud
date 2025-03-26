using Newtonsoft.Json;

namespace ComShopApp.Models.Backend.Inmueble;

public class CategoryResponse
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("nombre")]
    public string CategoryName { get; set; }
    [JsonProperty("imageUrl")]
    public string ImageUrl { get; set; }
}
