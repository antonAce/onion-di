using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using OnionDI.Domain.Models;
using OnionDI.Domain.Repositories;

using OnionDI.Data.EF.Context;

namespace OnionDI.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private bool _isDisposed = false;
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = _context.Products.ToArray();

            foreach (var product in products)
                await LoadEntityProperties(product);

            return products;
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            var product = await _context.Products.FindAsync(id);
            
            if (product != null)
                await LoadEntityProperties(product);

            return product;
        }

        public async Task CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            await Task.Run(() => { _context.Products.Update(product); });
        }

        public async Task DeleteAsync(string id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
                _context.Products.Remove(product);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
                _context.Dispose();

            _isDisposed = true;
        }
        
        private async Task LoadEntityProperties(Product product)
        {
            await _context.Entry(product)
                .Collection(p => p.OrderProducts)
                .LoadAsync();

            var orderProducts = product.OrderProducts;
            
            var propLoading = new List<Task>();
            
            foreach (var op in orderProducts)
                propLoading.Add(_context.Entry(op)
                    .Reference(o => o.Order)
                    .LoadAsync());

            await Task.WhenAll(propLoading);
        }
    }
}