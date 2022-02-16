﻿using PCLabs.EntityDesign.Domain.Contracts;
using PCLabs.EntityDesign.Domain.Orders;

namespace PCLabs.EntityDesign.Api.DataAccess.Seeds
{
    public static class OrderSeed
    {
        public static List<Order> GetOrders(IDateTime dateTimeService)
        {
            var orders = new List<Order>();

            orders.Add(GenerateOrder(1, 1.ToString("D6"), dateTimeService));
            orders.Add(GenerateOrder(2, 2.ToString("D6"), dateTimeService));
            orders.Add(GenerateOrder(3, 3.ToString("D6"), dateTimeService));
            orders.Add(GenerateOrder(4, 4.ToString("D6"), dateTimeService));
            orders.Add(GenerateOrder(5, 5.ToString("D6"), dateTimeService));

            return orders;
        }

        public static Order GenerateOrder(int index, string orderNo, IDateTime dateTimeService)
        {
            var customer = new Customer($"FirstName{index}", $"LastName{index}", $"person{index}@local.com");
            var address = new Address($"Street {index}", $"City{index}", $"1{index.ToString("D4")}", $"Country{index}");

            var order = new Order(dateTimeService, orderNo, customer, address);

            order.AddItem(new OrderItem($"item{index}-1", 50m));
            order.AddItem(new OrderItem($"item{index}-2", 70m));
            order.AddItem(new OrderItem($"item{index}-3", 100m));

            return order;
        }
    }
}
