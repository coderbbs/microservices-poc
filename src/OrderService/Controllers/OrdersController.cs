using Microsoft.AspNetCore.Mvc;
using OrderService.Contracts;
using OrderService.Domain;
using OrderService.Infrastructure.Messaging;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            try
            {
                // Intentionally async – order processing is event-driven
                var order = new Order(Guid.NewGuid());

            var orderCreatedEvent = new OrderCreatedEvent(
                order.Id,
                order.CreatedAt
            );

            var publisher = new EventPublisher();
            await publisher.PublishAsync(orderCreatedEvent);

            return Accepted();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message+ "Order creation failed");
                return StatusCode(500, "Order processing failed");
            }
        }
    }
}
