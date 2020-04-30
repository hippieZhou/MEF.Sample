using BlackApp.Application.Contracts;
using BlackApp.Application.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlankApp
{
    public sealed class BusinessModuleLoader : IBusinessModuleLoader
    {
        private readonly IModuleCatalog _moduleCatalog;
        private readonly IRegionManager _regionManager;
        private readonly IModuleManager _moduleManager;


        public BusinessModuleLoader(
        IRegionManager regionManager,
        IModuleCatalog moduleCatalog,
        IModuleManager moduleManager)
        {
            _moduleCatalog = moduleCatalog ?? throw new ArgumentNullException(nameof(moduleCatalog));
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            _moduleManager = moduleManager ?? throw new ArgumentNullException(nameof(moduleManager));
        }

        public IEnumerable<BusinessModuleEntity> Modules
        {
            get
            {
                var modules = new List<BusinessModuleEntity>();

                _moduleCatalog.Modules.ToList().ForEach(info =>
                {
                    if (Type.GetType(info.ModuleType).GetCustomAttributes(typeof(BusinessModuleAttribute), true).FirstOrDefault()
                    is BusinessModuleAttribute meta)
                    {
                        modules.Add(new BusinessModuleEntity
                        {
                            Module = info,
                            Priority = meta.Priority,
                            Header = meta.Belong,
                            FriendlyName = meta.FriendlyName,
                        });
                    }
                });
                return modules;
            }
        }


        public void ActivateModuleView(BusinessModuleEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Module == null)
                throw new ArgumentNullException(nameof(entity.Module));

            if (entity.Module.State != ModuleState.Initialized)
            {
                _moduleManager.LoadModule(entity.Module.ModuleName);
            }
            _regionManager.RequestNavigate(RegionContracts.MainContentRegion, entity.Module.ModuleName);
        }
    }
}
