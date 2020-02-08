using System.Collections.Generic;
using System.Threading.Tasks;

using OnionDI.Business.Infrastructure.DTO;
using OnionDI.Business.Infrastructure.Interfaces;

using OnionDI.Domain.Models;
using OnionDI.Domain.Repositories;
using OnionDI.Domain.UnitOfWork;

namespace OnionDI.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public ProductService(IUnitOfWork unitOfWork,
                              IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }
        
        public async Task CreateProduct(ProductDto product)
        {
            await _productRepository.CreateAsync(DtoToProduct(product));
            await _unitOfWork.CommitAsync();
        }

        public async Task ModifyProduct(ProductDto product)
        {
            await _productRepository.UpdateAsync(DtoToProduct(product));
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveProduct(ProductDto product)
        {
            await _productRepository.DeleteAsync(product.Gtin);
        }

        public async Task<ProductDto> GetProductByGtin(string gtin)
        {
            var product = await _productRepository.GetByIdAsync(gtin);
            return ProductToDto(product);
        }

        public async IAsyncEnumerable<ProductDto> ListProducts(int limit, int offset)
        {
            var products = await _productRepository.GetSubsetAsync(limit, offset);
                        
            foreach (var product in products)
                yield return ProductToDto(product);
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

        private Product DtoToProduct(ProductDto product)
        {
            return new Product
            {
                Gtin = product.Gtin,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}