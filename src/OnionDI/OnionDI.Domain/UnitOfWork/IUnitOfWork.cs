using System;
using System.Threading.Tasks;

namespace OnionDI.Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }
}