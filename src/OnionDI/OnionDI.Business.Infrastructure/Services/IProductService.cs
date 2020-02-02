using System.Collections.Generic;
using System.Threading.Tasks;

using OnionDI.Business.Infrastructure.DTO;

namespace OnionDI.Business.Infrastructure.Services
{
    public interface IProductService
    {
        Task CreateProduct(ProductDto product);
        Task ModifyProduct(ProductDto product);
        Task RemoveProduct(ProductDto product);
        
        Task<ProductDto> GetProductByGtin(string gtin);
        Task<IEnumerable<ProductDto>> ListProducts(int limit, int offset);
    }
}