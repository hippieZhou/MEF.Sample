using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlankApp.Doamin.Context
{
    /// <summary>
    /// 主要负责数据库操作后的对应的保存操作相应接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Commit(CancellationToken cancellationToken);
        Task Rollback();
    }
}
