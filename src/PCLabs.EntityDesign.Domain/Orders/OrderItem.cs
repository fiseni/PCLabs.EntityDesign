namespace PCLabs.EntityDesign.Domain.Orders
{
    public class OrderItem
    {
        private OrderItem() { }

        public int Id { get; set; }

        public string Name { get; }
        public decimal Price { get; }

        public int OrderId { get; }

        public OrderItem(string name, decimal price)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            Name = name;
            Price = price;
        }
    }
}
