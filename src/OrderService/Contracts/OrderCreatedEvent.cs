namespace OrderService.Contracts;

public record OrderCreatedEvent
(
     Guid OrderId,
     DateTime CreatedAt 
);
