namespace PixelPortalen.Shared.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime ShipDate { get; set; }
    public List<OrderDetail> Details { get; set; }
}