using System.Text.Json.Serialization;

namespace PixelPortalen.Shared.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public ICollection<Product> Products { get; } = [];
}