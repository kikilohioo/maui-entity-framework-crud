using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComShopApp.Models.Backend.Inmueble;

public class InmuebleResponse
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("nombre")]
    public string Name { get; set; }

    [JsonProperty("direccion")]
    public string Address { get; set; }

    [JsonProperty("detalle")]
    public string Descripcion { get; set; }

    [JsonProperty("precio")]
    public decimal Price { get; set; }

    [JsonProperty("imagenUrl")]
    public string UrlImage { get; set; }

    [JsonProperty("usuarioId")]
    public Guid UserId { get; set; }

    [JsonProperty("categoryId")]
    public int CategoryId { get; set; }

    [JsonProperty("isTrending")]
    public bool IsTrending { get; set; }

    [JsonProperty("isBookmarkEnabled")]
    public bool IsBookmarkEnabled { get; set; }

    [JsonProperty("bookmarkUserId")]
    public string BookmarkUserId { get; set; }

    [JsonProperty("telefono")]
    public string Phone { get; set; }
}
