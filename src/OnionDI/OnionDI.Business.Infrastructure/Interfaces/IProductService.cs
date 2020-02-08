using System.Collections.Generic;
using System.Threading.Tasks;

using OnionDI.Business.Infrastructure.DTO;

namespace OnionDI.Business.Infrastructure.Interfaces
{
    public interface IProductService
    {
        Task CreateProduct(ProductDto product);
        Task ModifyProduct(ProductDto product);
        Task RemoveProduct(ProductDto product);
        
        Task<ProductDto> GetProductByGtin(string gtin);
        IAsyncEnumerable<ProductDto> ListProducts(int? limit, int? offset);
    }
}