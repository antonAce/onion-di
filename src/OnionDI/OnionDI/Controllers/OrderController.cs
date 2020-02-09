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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetOrders(
            [FromQuery(Name = "from")] int? from,
            [FromQuery(Name = "to")] int? to)
        {
            try
            {
                int? limit = (to.HasValue && from.HasValue) ? (to - from) : null;
                var orders = await _orderService
                    .ListOrders(limit, from)
                    .ToArrayAsync();
                return Ok(orders);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Oops, something went wrong!");
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderById(id);
                return Ok(order);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Oops, something went wrong!");
            }
        }
        
        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetProductsOfOrder(int id)
        {
            try
            {
                var products = await _orderService
                    .GetProductsOfOrder(new OrderDto {Id = id})
                    .ToArrayAsync();

                if (products.Length == 0)
                    return NoContent();
                else
                    return Ok(products);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Oops, something went wrong!");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderToCreate order)
        {
            try
            {
                await _orderService.CreateOrder(new OrderDto { OrderingDate = order.OrderingDate });
                return Ok("New order was successfully created!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Oops, something went wrong!");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyOrder(int id, [FromBody] OrderToModify order)
        {
            try
            {
                await _orderService.ChangeOrder(
                    new OrderDto
                    {
                        Id = id,
                        OrderingDate = order.OrderingDate
                    }
                );
                return Ok("Product was successfully added to the order!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Oops, something went wrong!");
            }
        }
        
        [HttpPut("{id}/products")]
        public async Task<IActionResult> AddProductToOrder(int id, [FromBody] ProductDto product)
        {
            try
            {
                await _orderService.AddProductToTheOrder(
                    new OrderDto { Id = id },
                    product
                    );
                return Ok("Product was successfully added to the order!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Oops, something went wrong!");
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveOrder(int id)
        {
            try
            {
                await _orderService.DeleteOrder(new OrderDto {Id = id});
                return Ok("Order was successfully removed!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Oops, something went wrong!");
            }
        }
        
        [HttpDelete("{id}/products/{gtin}")]
        public async Task<IActionResult> RemoveProductFromOrder(int id, string gtin)
        {
            try
            {
                await _orderService.RemoveProductFromTheOrder(
                    new OrderDto { Id = id }, 
                    new ProductDto { Gtin = gtin });
                return Ok("Product was successfully removed to the order!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Oops, something went wrong!");
            }
        }
    }
}