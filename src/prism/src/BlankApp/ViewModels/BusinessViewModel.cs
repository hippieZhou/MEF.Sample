using BlankApp.Doamin.Modularity;
using Prism.Modularity;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlankApp.Models
{
    public class Node
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public Node(Guid id,string title)
        {
            Id = id;
            Title = title;
        }
    }

    public class BusinessViewModel : BindableBase
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        private int? _priority;
        public int? Priority
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

    public static class BusinessExtension
    {
        public static BusinessViewModel ToBusiness(this IModuleInfo module)
        {
            if (module == null)
                throw new ArgumentNullException(nameof(module));

            var metaData = Type.GetType(module.ModuleType)
                         .GetCustomAttributes(typeof(BusinessModuleAttribute), true)
                         .FirstOrDefault() as BusinessModuleAttribute;
            return new BusinessViewModel()
            {
                Priority = metaData?.Priority,
                Header = metaData?.MainMenu,
                FriendlyName = metaData?.FriendlyName,
                Module = module
            };
        }

        public static IEnumerable<string> GetHeaders(this IEnumerable<BusinessViewModel> businesses)
        {
            if (businesses == null)
                throw new ArgumentNullException(nameof(businesses));

            return businesses.OrderBy(x => x.Priority).GroupBy(x => x.Header).Where(x => !string.IsNullOrWhiteSpace(x.Key)).Select(x => x.Key);
        }

        public static IEnumerable<Node> GetNodes(this IEnumerable<BusinessViewModel> businesses, string header)
        {
            if (businesses == null)
                throw new ArgumentNullException(nameof(businesses));

            return businesses.GroupBy(x => x.Header).Where(x => x.Key == header).SingleOrDefault().AsEnumerable().Select(x => new Node(x.Id,x.FriendlyName));
        }
    }
}
