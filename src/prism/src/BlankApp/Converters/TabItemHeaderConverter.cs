using BlackApp.Application.Framework;
using BlackApp.Application.Modularity;
using Prism.Regions;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace BlankApp.Converters
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/archive/blogs/dphill/closable-tabbed-views-in-prism
    /// https://github.com/brianlagunas/XamTabControlAsPrismRegion/blob/master/TabControlRegion.Core/TabItemRemoveBehavior.cs
    /// https://github.com/PrismLibrary/Prism/issues/327
    /// </summary>
    public class TabItemHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = value.GetType();
            var moduleLoader = EnginContext.Current.Resolve<IBusinessModuleLoader>();
            var module = moduleLoader.Modules.FirstOrDefault(x => string.Equals(x.Module.Ref, type.Assembly.CodeBase, StringComparison.OrdinalIgnoreCase));
            var sp = new StackPanel { Orientation = Orientation.Horizontal };
            sp.Children.Add(new TextBlock { Text = module != null ? module.FriendlyName : (default) });
            var close = new Button { Margin = new Thickness(6, 0, 0, 0), Content = "关闭" };
            close.Click += (sender, args) =>
            {
                TabItem tabItem = FindVisualParent<TabItem>(args.OriginalSource as DependencyObject);
                TabControl tabControl = FindVisualParent<TabControl>(tabItem);
                if (tabItem != null && tabControl != null)
                {
                    object view = tabItem.Content;
                    IRegion region = RegionManager.GetObservableRegion(tabControl).Value;
                    if (region != null && CanRemoveItem(view, region))
                    {
                        region.Remove(view);
                    }
                }
            };
            sp.Children.Add(close);
            return sp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private T FindVisualParent<T>(DependencyObject node) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(node);
            if (parent == null | parent is T)
            {
                return (T)parent;
            }

            // Recurse up the visual tree.
            return FindVisualParent<T>(parent);
        }


        private bool CanRemoveItem(object item, IRegion region)
        {
            bool canRemove = true;

            var context = new NavigationContext(region.NavigationService, null);

            if (item is IConfirmNavigationRequest confirmRequestItem)
            {
                confirmRequestItem.ConfirmNavigationRequest(context, result =>
                {
                    canRemove = result;
                });
            }

            if (item is FrameworkElement frameworkElement && canRemove)
            {
                if (frameworkElement.DataContext is IConfirmNavigationRequest confirmRequestDataContext)
                {
                    confirmRequestDataContext.ConfirmNavigationRequest(context, result =>
                    {
                        canRemove = result;
                    });
                }
            }

            return canRemove;
        }
    }
}
