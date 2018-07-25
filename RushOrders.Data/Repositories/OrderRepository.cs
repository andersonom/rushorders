using MongoDB.Driver;
using RushOrders.Core.Interfaces.Repositories;
using RushOrders.Core.Models;
using RushOrders.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RushOrders.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoContext _mongoContext;

        public OrderRepository(IMongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            var orders = _mongoContext.Database.GetCollection<Order>("Order");

            return await orders.Find(i => i.Customer.Id == customerId).ToListAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            var orders = _mongoContext.Database.GetCollection<Order>("Order");

            await orders.InsertOneAsync(order);
        }
    }
}
