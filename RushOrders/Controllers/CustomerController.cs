﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RushOrders.Core.Interfaces.Services;
using RushOrders.Core.Models;

namespace RushOrders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;

        public CustomerController(ICustomerService customerService, IOrderService orderService)
        {
            _customerService = customerService;
            _orderService = orderService;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _customerService.GetAllAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            if (customer != null)
            {
                return Ok(customer);
            }

            return NotFound();
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerService.AddAsync(customer);
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}
