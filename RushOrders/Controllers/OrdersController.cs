using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Post([FromBody] Order order, int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer != null)
            {
                order.Customer = customer;
                if (ModelState.IsValid)
                {               
                    await _orderRepository.AddOrderAsync(order);
                    return Ok();
                }
                return BadRequest(order);
            }

            return NotFound("Customer Not Found");
        }

        // GET api/values/5        
        [HttpGet]
        public async Task<List<Order>> Get(int customerId)
        {
            return await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
        }
    }
}
