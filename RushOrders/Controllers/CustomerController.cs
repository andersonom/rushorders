using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RushOrders.Core.Interfaces.Repositories;
using RushOrders.Core.Models;

namespace RushOrders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public CustomerController(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _customerRepository.GetAllAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<dynamic> Get(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer != null)
            {
                var orders = await _orderRepository.GetOrdersByCustomerIdAsync(id);

                return new { Customer = customer, Orders = orders };
            }

            return NotFound();
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerRepository.AddAsync(customer);
                return Ok();
            }
            return BadRequest(customer);
        }
    }
}
