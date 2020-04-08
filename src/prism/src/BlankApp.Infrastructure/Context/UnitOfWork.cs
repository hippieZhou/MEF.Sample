using BlankApp.Doamin.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlankApp.Infrastructure.Context
{
    /// <summary>
    /// 该类请勿直接使用，请通过注入 IUnitOfWork 的方式来使用
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public Task<int> Commit(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Rollback()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
