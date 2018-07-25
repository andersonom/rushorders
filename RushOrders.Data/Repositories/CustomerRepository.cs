using RushOrders.Core.Interfaces.Repositories;
using RushOrders.Core.Models;
using RushOrders.Data.Context;

namespace RushOrders.Data.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(SqlContext context) : base(context)
        {
                
        }
    }
}
