using Microsoft.AspNetCore.Mvc;
using PixelPortalen.API.Services;
using PixelPortalen.Shared.DTO;
using PixelPortalen.Shared.Models;

namespace PixelPortalen.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService orderService;

        public OrdersController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            var orders = await orderService.GetAll();

            if (orders == null) return NotFound("Det finns inga registrerade ordrar.");

            return Ok(orders);
        }

        [HttpGet("id/{orderId}")]
        public async Task<ActionResult<Order>> GetById(int orderId)
        {
            var order = await orderService.Get(orderId);

            if (order == null) return NotFound("Det finns ingen order på detta Id.");

            return Ok(order);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrdersByCustomerId(int customerId)
        {
            var orders = await orderService.GetAllOrdersByCustomerId(customerId);

            if (orders == null || !orders.Any())
            {
                return NotFound($"Inga ordrar hittades för kund med ID {customerId}.");
            }

            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] OrderDTO orderDto)
        {
            if (orderDto == null || orderDto.Details == null || !orderDto.Details.Any())
            {
                return BadRequest("Ordern måste innehålla minst en orderrad.");
            }

            var createdOrder = await orderService.CreateOrder(orderDto);

            return CreatedAtAction(nameof(GetAllOrdersByCustomerId), new { customerId = createdOrder.CustomerId }, createdOrder);
        }

        [HttpPatch("{orderId}/shipdate")]
        public async Task<IActionResult> EditOrder(int orderId, [FromBody] UpdateShipDateDTO dto)
        {
            var update = await orderService.UpdateShipDate(orderId, dto.ShipDate);

            if (!update) return NotFound($"Ingen order hittades på ID:{orderId}.");

            return NoContent();
        }
    }
}