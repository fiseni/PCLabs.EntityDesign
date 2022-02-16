namespace PCLabs.EntityDesign.Api.Orders.Models
{
    public class OrderCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public List<OrderItemCreateDto> Items { get; set; }
    }
}
