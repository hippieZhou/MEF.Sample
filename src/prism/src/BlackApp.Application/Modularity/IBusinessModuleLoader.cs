using System.Collections.Generic;

namespace BlackApp.Application.Modularity
{
    public interface IBusinessModuleLoader
    {
        IEnumerable<BusinessModuleEntity> Modules { get; }
        void ActivateModuleView(BusinessModuleEntity module);
    }
}
