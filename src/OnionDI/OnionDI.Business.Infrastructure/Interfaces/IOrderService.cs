using System.Collections.Generic;
using System.Threading.Tasks;

using OnionDI.Business.Infrastructure.DTO;

namespace OnionDI.Business.Infrastructure.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(OrderDto order);
        Task ChangeOrder(OrderDto order);
        Task DeleteOrder(OrderDto order);

        Task AddProductToTheOrder(OrderDto order, ProductDto product);
        Task RemoveProductFromTheOrder(OrderDto order, ProductDto product);
        
        IAsyncEnumerable<ProductDto> GetProductsOfOrder(OrderDto order);
        IAsyncEnumerable<OrderDto> ListOrders(int limit, int offset);
        Task<OrderDto> GetOrderById(int id);
    }
}