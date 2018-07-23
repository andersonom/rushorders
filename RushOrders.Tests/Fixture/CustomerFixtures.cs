using RushOrders.Core.Models;
using System.Collections.Generic;

namespace RushOrders.Tests.Fixture
{
    public static class CustomerFixtures
    {
        public static List<Customer> GetCustomerList => new List<Customer>() {
            new Customer()
            {
                Id = 1,
                Name = "Anderson Martins",
                Email = "andersonom@gmail.com"
            }
        };
    }
}
