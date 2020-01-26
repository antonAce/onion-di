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
    public class ProductController : ControllerBase
    {
        public ProductController()
        {
            
        }
        
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok("");
        }
    }
}