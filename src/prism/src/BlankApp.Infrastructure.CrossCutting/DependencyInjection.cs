using BlankApp.Infrastructure.CrossCutting.Identity;
using BlankApp.Infrastructure.Identity;
using Prism.Ioc;

namespace BlankApp.Infrastructure.CrossCutting
{
    public static class DependencyInjection
    {
        public static IContainerRegistry AddCrossCutting(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IIdentityManager, IdentityManager>();
            return containerRegistry;
        }
    }
}
