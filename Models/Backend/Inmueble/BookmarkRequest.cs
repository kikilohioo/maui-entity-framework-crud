
using Newtonsoft.Json;

namespace ComShopApp.Models.Backend.Inmueble;

public class BookmarkRequest
{
    [JsonProperty("usuarioId")]
    public string UserId { get; set; }

    [JsonProperty("inmuebleId")]
    public int InmuebleId { get; set; }
}
