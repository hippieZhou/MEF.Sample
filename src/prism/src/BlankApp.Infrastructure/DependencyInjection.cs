using BlankApp.Doamin.Context;
using BlankApp.Infrastructure.Context;
using Prism.Ioc;

namespace BlankApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IContainerRegistry AddInfrastructure(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ApplicationDbContext>();
            containerRegistry.Register(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
            containerRegistry.Register<IUnitOfWork, UnitOfWork>();
            return containerRegistry;
        }
    }
}
