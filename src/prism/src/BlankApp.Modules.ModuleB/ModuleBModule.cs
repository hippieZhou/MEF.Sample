using BlankApp.Doamin.Contracts;
using BlankApp.Doamin.Modularity;
using BlankApp.Modules.ModuleB.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace BlankApp.Modules.ModuleB
{
    [Module(OnDemand = true)]
    [BusinessModule(MainMenu = MainMenuContracts.B, FriendlyName = "模块B")]
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