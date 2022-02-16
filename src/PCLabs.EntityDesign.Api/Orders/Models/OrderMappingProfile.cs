using AutoMapper;
using PCLabs.EntityDesign.Domain.Orders;

namespace PCLabs.EntityDesign.Api.Orders.Models
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Customer, OrderDto>();
            CreateMap<Address, OrderDto>();

            CreateMap<Order, OrderDto>()
                .IncludeMembers(x => x.Customer, x => x.Address);
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
