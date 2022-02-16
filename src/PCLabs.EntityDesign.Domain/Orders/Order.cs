using PCLabs.EntityDesign.Domain.Contracts;

namespace PCLabs.EntityDesign.Domain.Orders
{
    public class Order
    {
        private Order() { }

        public int Id { get; }

        public string OrderNo { get; }
        public DateTime DateCreated { get; }
        public DateTime? DateCompleted { get; private set; }

        public Customer Customer { get; }
        public Address Address { get; private set; }

        public decimal GrandTotal { get; private set; }


        private readonly List<OrderItem> _items = new List<OrderItem>();
        public IEnumerable<OrderItem> Items => _items.AsEnumerable();


        public Order(IDateTime dateTimeProvider, string orderNo, Customer customer, Address address)
        {
            if (dateTimeProvider is null) throw new ArgumentNullException(nameof(dateTimeProvider));
            if (customer is null) throw new ArgumentNullException(nameof(customer));
            if (string.IsNullOrEmpty(orderNo)) throw new ArgumentNullException(nameof(orderNo));

            OrderNo = orderNo;
            Customer = customer;
            Address = address;

            DateCreated = dateTimeProvider.UtcNow;
        }

        public void UpdateAddress(Address address)
        {
            if (address is null) throw new ArgumentNullException(nameof(address));

            if (Address is null || !Address.Equals(address))
            {
                Address = address;
            }
        }

        public void Complete(IDateTime dateTimeProvider)
        {
            if (dateTimeProvider is null) throw new ArgumentNullException(nameof(dateTimeProvider));

            DateCompleted = dateTimeProvider.UtcNow;
        }

        public OrderItem AddItem(OrderItem orderItem)
        {
            if (orderItem is null) throw new ArgumentNullException(nameof(orderItem));

            _items.Add(orderItem);

            CalculateGrandTotal();

            return orderItem;
        }

        public void DeleteItem(int orderItemId)
        {
            var orderItem = _items.FirstOrDefault(x=>x.Id == orderItemId);

            if (orderItem is null) throw new KeyNotFoundException($"The order item with Id: {orderItem} is not found!");

            _items.Add(orderItem);

            CalculateGrandTotal();
        }

        private void CalculateGrandTotal()
        {
            GrandTotal = Items.Sum(x => x.Price);
        }
    }
}
