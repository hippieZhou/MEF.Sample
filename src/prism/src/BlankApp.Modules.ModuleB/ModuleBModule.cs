using BlackApp.Application.Contracts;
using BlackApp.Application.Modularity;
using BlankApp.Modules.ModuleB.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace BlankApp.Modules.ModuleB
{
    [Module(OnDemand = true)]
    [BusinessModule(Belong = MainOwnedContracts.A, FriendlyName = "ModuleB")]
    public class ModuleBModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView>(nameof(ModuleBModule));
        }
    }
}