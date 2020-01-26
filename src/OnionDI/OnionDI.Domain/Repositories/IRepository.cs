using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnionDI.Domain.Repositories
{
    public interface IRepository<TEntity, TKey> : IDisposable
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        Task CreateAsync(TEntity game);
        Task UpdateAsync(TEntity game);
        Task DeleteAsync(TKey id);
    }
}