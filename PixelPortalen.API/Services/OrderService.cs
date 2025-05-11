using PixelPortalen.API.Repositories.Interfaces;
using PixelPortalen.Shared.Models;
using PixelPortalen.Shared.DTO;
using PixelPortalen.API.Repositories;


namespace PixelPortalen.API.Services
{
    public class OrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepo)
        {
            orderRepository = orderRepo;
        }
        public async Task<Order> Get(int orderId)
        {
            return await orderRepository.Get(orderId);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await orderRepository.GetAll();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersByCustomerId(int customerId)
        {
            return await orderRepository.GetAllOrdersByCustomerId(customerId);
        }

        public async Task<Order> CreateOrder(OrderDTO order)
        {
            var newOrder = new Order
            {
                CustomerId = order.CustomerId,
                ShipDate = order.ShipDate,
                OrderDate = order.OrderDate,
                Details = order.Details.Select(d => new OrderDetail
                {
                    Price = d.Price,
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    ProductName = d.ProductName
                }).ToList()
            };

            return await orderRepository.CreateOrder(newOrder);
        }


        public async Task<bool> UpdateShipDate(int orderId, DateTime newShipDate)
        {
            var order = await orderRepository.Get(orderId);

            if (order == null) return false;

            order.ShipDate = newShipDate;
            await orderRepository.Edit(order);
            return true;
        }
    }
}
