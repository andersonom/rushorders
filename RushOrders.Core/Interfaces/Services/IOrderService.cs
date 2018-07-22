using System.Collections.Generic;
using System.Threading.Tasks;
using RushOrders.Core.Models;

namespace RushOrders.Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId);
        Task<bool> AddOrderAsync(Order order, int customerId);
    }
}