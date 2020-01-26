using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OnionDI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        public OrderController()
        {
            
        }
        
        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok("");
        }
    }
}