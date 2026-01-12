using Microsoft.AspNetCore.Mvc;
using OrderService.Contracts;
using OrderService.Domain;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateOrder()
        {
            // Intentionally async – order processing is event-driven
            var order = new Order(Guid.NewGuid());

            var orderCreatedEvent=new OrderCreatedEvent(
                order.Id,
                order.CreatedAt
                );

            return Accepted(orderCreatedEvent);
        }
    }
}
