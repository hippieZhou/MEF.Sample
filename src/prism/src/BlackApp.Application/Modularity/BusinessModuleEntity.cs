using Prism.Modularity;
using Prism.Mvvm;
using System;

namespace BlackApp.Application.Modularity
{
    public sealed class BusinessModuleEntity : BindableBase
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        private int _priority;
        public int Priority
        {
            get { return _priority; }
            set { SetProperty(ref _priority, value); }
        }

        private string _header;
        public string Header
        {
            get { return _header; }
            set { SetProperty(ref _header, value); }
        }

        private string _role;
        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }

        private string _friendlyName;
        public string FriendlyName
        {
            get { return _friendlyName; }
            set { SetProperty(ref _friendlyName, value); }
        }

        private IModuleInfo _module;
        public IModuleInfo Module
        {
            get { return _module; }
            set { SetProperty(ref _module, value); }
        }
    }
}
