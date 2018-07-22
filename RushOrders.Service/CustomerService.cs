using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RushOrders.Core.Interfaces.Repositories;
using RushOrders.Core.Interfaces.Services;
using RushOrders.Core.Models;

namespace RushOrders.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            return await _customerRepository.GetByIdAsync(customerId);
        }

        public async Task AddAsync(Customer customer)
        {
            await _customerRepository.AddAsync(customer);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }
    }
}
