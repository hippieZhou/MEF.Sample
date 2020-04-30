using Prism.Ioc;

namespace BlackApp.Application
{
    /// <summary>
    ///  该层为应用层，核心业务处理应该放到该层中来进行接口定义和实现
    /// </summary>
    public static class DependencyInjection
    {
        public static IContainerRegistry RegisterApplication(this IContainerRegistry containerRegistry, IContainerProvider container)
        {
            return containerRegistry;
        }
    }
}
