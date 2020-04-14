using BlankApp.Doamin.Bus;
using BlankApp.Doamin.Context;
using BlankApp.Infrastructure.Bus;
using BlankApp.Infrastructure.Context;
using Prism.Ioc;

namespace BlankApp.Infrastructure
{
    /// <summary>
    /// 改类中主要用于定义一个基础设施相关的服务，比如数据库操作的具体实现、eventbus、缓存中间件等
    /// </summary>
    public static class DependencyInjection
    {
        public static IContainerRegistry RegisterInfrastructure(this IContainerRegistry containerRegistry)
        {
            //注册消息总线
            containerRegistry.RegisterSingleton<IEventBus, InMemoryBus>();

            containerRegistry.RegisterSingleton<ApplicationDbContext>();
            containerRegistry.Register(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
            containerRegistry.Register<IUnitOfWork, UnitOfWork>();
            return containerRegistry;
        }
    }
}
