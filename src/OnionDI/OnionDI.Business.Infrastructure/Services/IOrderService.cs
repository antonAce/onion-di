using System.Collections.Generic;
using System.Threading.Tasks;

using OnionDI.Business.Infrastructure.DTO;

namespace OnionDI.Business.Infrastructure.Services
{
    public interface IOrderService
    {
        Task CreateOrder(OrderDto order);
        Task ChangeOrder(OrderDto order);
        Task DeleteOrder(OrderDto order);

        Task AddProductToTheOrder(OrderDto order, ProductDto product);
        Task RemoveProductFromTheOrder(OrderDto order, ProductDto product);
        
        Task<ProductDto> GetProductsOfOrder(OrderDto order);
        Task<IEnumerable<OrderDto>> ListOrders(int limit, int offset);
        Task<OrderDto> GetOrderById(int id);
    }
}