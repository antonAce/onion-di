using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using OnionDI.Business.Infrastructure.Interfaces;
using OnionDI.Business.Infrastructure.DTO;

using OnionDI.Models;

namespace OnionDI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(
            [FromQuery(Name = "from")] int? from,
            [FromQuery(Name = "to")] int? to)
        {
            try
            {
                int? limit = (to.HasValue && from.HasValue) ? (to - from) : null;
                var products = await _productService
                    .ListProducts(limit, from)
                    .ToArrayAsync();
                return Ok(products);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Oops, something went wrong!");
            }
        }
        
        [HttpGet("{gtin}")]
        public async Task<IActionResult> GetProducts(string gtin)
        {
            try
            {
                var product = await _productService.GetProductByGtin(gtin);
                return Ok(product);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Oops, something went wrong!");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto product)
        {
            try
            {
                await _productService.CreateProduct(product);
                return Ok("New product was successfully added!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Oops, something went wrong!");
            }
        }

        [HttpPut("{gtin}")]
        public async Task<IActionResult> ModifyProduct(string gtin, [FromBody] ProductToModify product)
        {
            try
            {
                await _productService.ModifyProduct(new ProductDto
                {
                    Gtin = gtin,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price
                });
                return Ok("Product was successfully modified!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Oops, something went wrong!");
            }
        }
        
        [HttpDelete("{gtin}")]
        public async Task<IActionResult> DeleteProduct(string gtin)
        {
            try
            {
                await _productService.RemoveProduct(new ProductDto { Gtin = gtin });
                return Ok("Product was successfully removed!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Oops, something went wrong!");
            }
        }
    }
}