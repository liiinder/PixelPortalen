using PixelPortalen.Shared.Models;
using System.ComponentModel.DataAnnotations;

public class CustomerRating
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Customer Customer { get; set; }
    public Product Product { get; set; }
}
