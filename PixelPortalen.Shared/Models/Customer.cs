using PixelPortalen.Shared.DTO;

namespace PixelPortalen.Shared.Models;

public class Customer
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public AddressInfo Address { get; set; }
    public string? Password { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public string Roll { get; set; } = "User";
}