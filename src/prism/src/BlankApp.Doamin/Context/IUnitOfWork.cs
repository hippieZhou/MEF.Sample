using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlankApp.Doamin.Context
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Commit(CancellationToken cancellationToken);
        Task Rollback();
    }
}
