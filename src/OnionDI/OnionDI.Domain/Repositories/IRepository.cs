using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnionDI.Domain.Repositories
{
    public interface IRepository<TEntity, TKey> : IDisposable
    {
        Task<IEnumerable<TEntity>> GetSubsetAsync(int? limit, int? offset);
        Task<TEntity> GetByIdAsync(TKey id);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);
    }
}