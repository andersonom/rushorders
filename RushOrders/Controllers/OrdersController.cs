using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RushOrders.Core.Interfaces.Services;
using RushOrders.Core.Models;

namespace RushOrders.Controllers
{
    [Route("api/customer/{customerId}/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order, int customerId)
        {
                if (await _orderService.AddAsync(order, customerId))
                    return Ok();
           
            return BadRequest(order);
        }

        // GET api/values/5        
        [HttpGet]
        public async Task<IActionResult> Get(int customerId)
        {
            var orders = await _orderService.GetOrdersByCustomerIdAsync(customerId);
            if (orders != null && orders.Count > 0)
                return Ok(orders);

            return NotFound();
        }
    }
}
