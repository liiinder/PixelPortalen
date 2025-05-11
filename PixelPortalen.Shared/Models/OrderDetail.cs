using System.ComponentModel.DataAnnotations.Schema;

namespace PixelPortalen.Shared.Models;

public class OrderDetail
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
}