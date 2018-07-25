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
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public CustomerService(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer != null)
            {
                customer.Orders = await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
            }

            return customer;
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
