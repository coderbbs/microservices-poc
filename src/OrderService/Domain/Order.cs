namespace OrderService.Domain
{
    public class Order
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Status { get; private set; }

        private Order() { }

        public Order(Guid id)
        { 
            Id = id;
            CreatedAt = DateTime.UtcNow;
            Status = "Created";
        }
    }
}
