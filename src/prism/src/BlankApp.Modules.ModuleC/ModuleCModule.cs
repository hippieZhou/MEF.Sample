using BlankApp.Doamin.Contracts;
using BlankApp.Doamin.Modularity;
using BlankApp.Modules.ModuleC.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace BlankApp.Modules.ModuleC
{
    [Module(OnDemand = true)]
    [BusinessModule(MainMenu = MainMenuContracts.B, FriendlyName = "模块C")]
    public class ModuleCModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView>(nameof(ModuleCModule));
        }
    }
}