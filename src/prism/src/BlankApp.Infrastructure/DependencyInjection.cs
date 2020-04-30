using BlackApp.Application.Context;
using BlankApp.Infrastructure.Context;
using Prism.Ioc;
using System;

namespace BlankApp.Infrastructure
{
    /// <summary>
    /// 改类中主要用于定义一个基础设施相关的服务，比如数据库操作的具体实现、eventbus、缓存中间件等
    /// </summary>
    public static class DependencyInjection
    {
        public static IContainerRegistry RegisterInfrastructure(this IContainerRegistry containerRegistry, IContainerProvider container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            containerRegistry.RegisterSingleton<ApplicationDbContext>();
            containerRegistry.RegisterSingleton<ApplicationIdentityDbContext>();
            containerRegistry.RegisterSingleton<IApplicationDbContext, ApplicationDbContext>();
            containerRegistry.Register(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
            containerRegistry.Register<IUnitOfWork, UnitOfWork>();
            return containerRegistry;
        }
    }
}
