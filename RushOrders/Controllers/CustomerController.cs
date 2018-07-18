using System;
using System.Collections.Generic;
using System.Linq;
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

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _customerRepository.GetAllAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Customer> Get(int id)
        {
           return await _customerRepository.GetByIdAsync(id);            
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] Customer value)
        {
            await _customerRepository.AddAsync(value);
        }
    }
}
