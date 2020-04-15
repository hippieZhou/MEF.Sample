using BlankApp.Doamin.Context;
using BlankApp.Infrastructure.Context;
using BlankApp.Infrastructure.Settings;
using Prism.Ioc;
using Prism.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

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

            #region 注入全局配置
            containerRegistry.RegisterInstance<ISettingsReader>(
                new SettingsReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json")));

            var settings = Assembly.GetExecutingAssembly()
             .GetTypes()
             .Where(t => t.Name.EndsWith(SettingsReader.DefaultSectionNameSuffix, StringComparison.InvariantCulture))
             .ToList();

            settings.ForEach(type =>
            {
                var instance = container.Resolve<ISettingsReader>().LoadSection(type);
                if (instance == null)
                {
                    container.Resolve<ILoggerFacade>()?.Log($"{type} 配置出错", Category.Exception, Priority.High);
                }
                containerRegistry.RegisterInstance(type, instance);
            });
            #endregion

            containerRegistry.RegisterSingleton<ApplicationDbContext>();
            containerRegistry.Register(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
            containerRegistry.Register<IUnitOfWork, UnitOfWork>();
            return containerRegistry;
        }
    }
}
