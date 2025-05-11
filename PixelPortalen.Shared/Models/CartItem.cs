using System.ComponentModel.DataAnnotations.Schema;

namespace PixelPortalen.Shared.Models
{
    public class CartItem
    {
        public int Id { get; set; }
    
        public string UserId { get; set; }
        public int ProductId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Product? Product { get; set; }
    }
}
