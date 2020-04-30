using BlackApp.Application.Contracts;
using BlackApp.Application.Modularity;
using BlankApp.Modules.ModuleA.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace BlankApp.Modules.ModuleA
{
    [Module(OnDemand = true)]
    [BusinessModule(Belong = MainOwnedContracts.A, FriendlyName = "ModuleA")]
    public class ModuleAModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView>(nameof(ModuleAModule));
        }
    }
}