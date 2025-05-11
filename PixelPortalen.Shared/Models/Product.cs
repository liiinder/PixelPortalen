using System.ComponentModel.DataAnnotations.Schema;

namespace PixelPortalen.Shared.Models;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
    public string? ImagePath { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    public int? Stock { get; set; }
    public Category Category { get; set; }
    public ICollection<Genre>? Genres { get; set; }

}