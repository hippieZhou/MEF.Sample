using BlankApp.Infrastructure.CrossCutting.Identity;
using BlankApp.Infrastructure.Identity;
using Prism.Ioc;

namespace BlankApp.Infrastructure.CrossCutting
{
    /// <summary>
    /// 该项目主要存放一些关注点分离的相关操作，它不涉及具体的业务，但是又影响着所有的业务，此时应考虑放到这个项目中来实现
    /// </summary>
    public static class DependencyInjection
    {
        public static IContainerRegistry RegisterCrossCutting(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IIdentityManager, IdentityManager>();
            return containerRegistry;
        }
    }
}
