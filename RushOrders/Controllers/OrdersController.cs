using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RushOrders.Core.Models;

namespace RushOrders.Controllers
{
    [Route("api/customer/{customerId}/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
       

        // POST: api/Orders
        [HttpPost]
        public void Post([FromBody] Orders order, int customerId)
        {
            //Add a  new Order  for  an existing  Customer
        }

    }
}
