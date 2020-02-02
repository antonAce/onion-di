using System;
using System.Threading.Tasks;

using OnionDI.Domain.UnitOfWork;

using OnionDI.Data.EF.Context;

namespace OnionDI.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _isDisposed = false;
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
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
    }
}