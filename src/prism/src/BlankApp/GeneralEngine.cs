using BlankApp.Doamin.Framework;
using Prism.Ioc;
using System;

namespace BlankApp
{
    public sealed class GeneralEngine : IEngine
    {
        private readonly Lazy<IContainerProvider> _container;
        public GeneralEngine(IContainerProvider container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }
            _container = new Lazy<IContainerProvider>(() => container);
        }

        public T Resolve<T>() where T : class => _container.Value.Resolve<T>();

        public T Resolve<T>(string name) where T : class => _container.Value.Resolve<T>(name);
    }
}
