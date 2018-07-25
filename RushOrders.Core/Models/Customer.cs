﻿using System.Collections.Generic;

namespace RushOrders.Core.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<Order> Orders { get; set; }
    }
}
