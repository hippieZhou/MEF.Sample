using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlackApp.Application.Context
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Commit(CancellationToken cancellationToken);
        Task Rollback();
    }
}
