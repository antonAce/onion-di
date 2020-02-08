using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OnionDI.Business.Infrastructure.DTO;
using OnionDI.Business.Infrastructure.Interfaces;

using OnionDI.Domain.Models;
using OnionDI.Domain.Repositories;
using OnionDI.Domain.UnitOfWork;

namespace OnionDI.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public OrderService(IUnitOfWork unitOfWork, 
                            IOrderRepository orderRepository, 
                            IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }
        
        public async Task CreateOrder(OrderDto order)
        {
            await _orderRepository.CreateAsync(DtoToOrder(order));
            await _unitOfWork.CommitAsync();
        }

        public async Task ChangeOrder(OrderDto order)
        {
            await _orderRepository.UpdateAsync(DtoToOrder(order));
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteOrder(OrderDto order)
        {
            await _orderRepository.DeleteAsync(order.Id);
        }

        public async Task AddProductToTheOrder(OrderDto order, ProductDto product)
        {
            var orderToModify = await _orderRepository.GetByIdAsync(order.Id);
            var productToAdd = await _productRepository.GetByIdAsync(product.Gtin);
            
            orderToModify.OrderProducts.Add(new OrderProduct
            {
                Order = orderToModify,
                Product = productToAdd
            });

            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveProductFromTheOrder(OrderDto order, ProductDto product)
        {
            var orderToModify = await _orderRepository.GetByIdAsync(order.Id);
            var productToRemove = await _productRepository.GetByIdAsync(product.Gtin);
            
            orderToModify.OrderProducts.Remove(new OrderProduct
            {
                Order = orderToModify,
                Product = productToRemove
            });

            await _unitOfWork.CommitAsync();
        }

        public async IAsyncEnumerable<ProductDto> GetProductsOfOrder(OrderDto order)
        {
            var orderToReturn = await _orderRepository.GetByIdAsync(order.Id);
            var products = orderToReturn.OrderProducts.Select(op => op.Product);

            foreach (var product in products)
                yield return ProductToDto(product);
        }

        public async IAsyncEnumerable<OrderDto> ListOrders(int? limit = null, int? offset = null)
        {
            var orders = await _orderRepository.GetSubsetAsync(limit, offset);
            
            foreach (var order in orders)
                yield return OrderToDto(order);
        }

        public async Task<OrderDto> GetOrderById(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return OrderToDto(order);
        }
        
        private OrderDto OrderToDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                OrderingDate = order.OrderingDate
            };
        }
        
        private ProductDto ProductToDto(Product product)
        {
            return new ProductDto
            {
                Gtin = product.Gtin,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price
            };
        }

        private Order DtoToOrder(OrderDto order)
        {
            return new Order
            {
                Id = order.Id,
                OrderingDate = order.OrderingDate
            };
        }
    }
}