using PixelPortalen.Shared.Models;

namespace PixelPortalen.API.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>?> GetAllOrdersByCustomerId(int customerId);
        Task<Order> CreateOrder(Order order);
        Task EditOrder(Order order);
    }
}
