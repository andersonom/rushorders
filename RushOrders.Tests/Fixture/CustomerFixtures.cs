using System;
using RushOrders.Core.Models;
using System.Collections.Generic;

namespace RushOrders.Tests.Fixture
{
    public static class CustomerFixtures
    {
        public static List<Customer> GetCustomerList => new List<Customer>() {
            new Customer()
            { 
                Name = "Anderson Martins",
                Email = "andersonom@gmail.com"
            }
        };
        public static Customer GetCustomerWithInvalidEmail => new Customer()
        {
            Id = 1,
            Name = "Anderson Martins",
            Email = "andersonomgmail.com"
        };
        public static Customer GetCustomerWithInvalidName => new Customer()
        {
            Id = 1,
            Name = String.Empty,
            Email = "andersonom@gmail.com"
        };
    }
}
