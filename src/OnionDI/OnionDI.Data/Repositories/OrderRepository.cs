using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using OnionDI.Domain.Models;
using OnionDI.Domain.Repositories;

using OnionDI.Data.EF.Context;

namespace OnionDI.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private bool _isDisposed = false;
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var orders = _context.Orders.ToArray();

            foreach (var order in orders)
                await LoadEntityProperties(order);

            return orders;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            
            if (order != null)
                await LoadEntityProperties(order);

            return order;
        }

        public async Task CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task UpdateAsync(Order order)
        {
            await Task.Run(() => { _context.Orders.Update(order); });
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order != null)
                _context.Orders.Remove(order);
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

        private async Task LoadEntityProperties(Order order)
        {
            await _context.Entry(order)
                .Collection(o => o.OrderProducts)
                .LoadAsync();

            var orderProducts = order.OrderProducts;
            
            var propLoading = new List<Task>();
            
            foreach (var op in orderProducts)
                propLoading.Add(_context.Entry(op)
                    .Reference(o => o.Product)
                    .LoadAsync());

            await Task.WhenAll(propLoading);
        }
    }
}