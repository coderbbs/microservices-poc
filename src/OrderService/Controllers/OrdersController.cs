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
    }
}
