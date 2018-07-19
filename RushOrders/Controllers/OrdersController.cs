using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RushOrders.Core.Interfaces.Repositories;
using RushOrders.Core.Models; 

namespace RushOrders.Controllers
{
    [Route("api/customer/{customerId}/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrdersController(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }
        // POST: api/Orders
        [HttpPost]
        public async Task Post([FromBody] Order order, int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            order.Customer = customer;
            await _orderRepository.AddOrderAsync(order);            
        }

        // GET api/values/5        
        [HttpGet]
        public async Task<List<Order>> Get(int customerId)
        {
            return await _orderRepository.GetOrdersByCustomerIdAsync(customerId);            
        }
    }
}
