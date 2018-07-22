using System.Collections.Generic;
using System.Threading.Tasks;
using RushOrders.Core.Models;

namespace RushOrders.Core.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetByIdAsync(int customerId);
        Task AddAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllAsync();
    }
}