using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using RushOrders.Core.Interfaces.Repositories;
using RushOrders.Core.Models;

namespace RushOrders.Tests.Mock
{

    public class OrderMockedRepository : IOrderRepository
    {
        private List<Order> orders;
        public OrderMockedRepository()
        {
            orders = new List<Order>();
        }
        public async Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return orders.Where(i => i.Customer.Id == customerId).ToList();
        }

        public async Task AddOrderAsync(Order order)
        {
            order.Id = ObjectId.GenerateNewId().ToString(); 
            orders.Add(order);
        }
    }
}
