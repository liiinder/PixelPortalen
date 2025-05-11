using Microsoft.EntityFrameworkCore;
using PixelPortalen.API.Data;
using PixelPortalen.API.Repositories.Interfaces;
using PixelPortalen.Shared.Models;

namespace PixelPortalen.API.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private APIDbContext? orderContext => context as APIDbContext;

        public OrderRepository(APIDbContext context) : base(context)
        {
        }

        public new async Task<IEnumerable<Order>> GetAll()
        {
            return await orderContext.Orders
                .Include(o => o.Details)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>?> GetAllOrdersByCustomerId(int customerId)
        {
            return await orderContext.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.Details)
                .ToListAsync();
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await orderContext.Orders.AddAsync(order);
            await orderContext.SaveChangesAsync();
            return order;
        }

        public async Task EditOrder(Order order)
        {
            orderContext.Orders.Update(order);
            await orderContext.SaveChangesAsync();
        }
    }
}
