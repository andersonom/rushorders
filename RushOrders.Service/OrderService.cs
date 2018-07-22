using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RushOrders.Core.Interfaces.Repositories;
using RushOrders.Core.Interfaces.Services;
using RushOrders.Core.Models;

namespace RushOrders.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
        }

        public async Task<bool> AddOrderAsync(Order order, int customerId)
        {
            Customer customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer == null) return false;

            order.Customer = customer;

            await _orderRepository.AddOrderAsync(order);

            return true;
        }
    }
}
