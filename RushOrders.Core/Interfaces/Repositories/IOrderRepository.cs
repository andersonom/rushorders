using RushOrders.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RushOrders.Core.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId);

        Task AddOrderAsync(Order order);
    }
}
