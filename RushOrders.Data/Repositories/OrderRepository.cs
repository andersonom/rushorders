using MongoDB.Driver;
using RushOrders.Core.Interfaces.Repositories;
using RushOrders.Core.Models;
using RushOrders.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RushOrders.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MongoContext mongoContext;

        public OrderRepository(MongoContext _mongoContext)
        {
            mongoContext = _mongoContext;
        }
                 
        public async Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            var orders = mongoContext.Database.GetCollection<Order>("Order");
            return await orders.Find(i => i.Customer.Id == customerId).ToListAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            var orders = mongoContext.Database.GetCollection<Order>("Order");

            order.CreationDate = DateTime.UtcNow; //Maybe can be added to database configuration rule like in EF, check mongo
            await orders.InsertOneAsync(order);
        }
    }
}
