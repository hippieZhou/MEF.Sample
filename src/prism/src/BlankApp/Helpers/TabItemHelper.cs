using BlackApp.Application.Framework;
using BlackApp.Application.Modularity;
using Prism.Regions;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BlankApp.Helpers
{
    public class TabItemHelper
    {
        public static object GetHeader(DependencyObject obj)
        {
            return (object)obj.GetValue(HeaderProperty);
        }

        public static void SetHeader(DependencyObject obj, object value)
        {
            obj.SetValue(HeaderProperty, value);
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.RegisterAttached("Header", typeof(object), typeof(TabItemHelper), new PropertyMetadata(default,(d,e)=> 
            {
                if (d is TabItem handler && e.NewValue != null)
                {
                    var type = e.NewValue.GetType();
                    var moduleLoader = EnginContext.Current.Resolve<IBusinessModuleLoader>();
                    var module = moduleLoader.Modules.FirstOrDefault(x => string.Equals(x.Module.Ref, type.Assembly.CodeBase, StringComparison.OrdinalIgnoreCase));
                    if (module == null)
                    {
                        throw new ArgumentException($"目标模块未发现：{type.Name}");
                    }

                    var sp = new StackPanel { Orientation = Orientation.Horizontal };
                    sp.Children.Add(new TextBlock { VerticalAlignment = VerticalAlignment.Center, Text = module != null ? module.FriendlyName : (default) });
                    var close = new Button { VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(12, 0, 0, 0), Content = "关闭" };
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
                    handler.Header = sp;
                }
            }));


        private static T FindVisualParent<T>(DependencyObject node) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(node);
            if (parent == null | parent is T)
            {
                return (T)parent;
            }

            // Recurse up the visual tree.
            return FindVisualParent<T>(parent);
        }


        private static bool CanRemoveItem(object item, IRegion region)
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
