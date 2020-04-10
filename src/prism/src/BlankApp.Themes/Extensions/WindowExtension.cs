using System.Windows;

namespace BlankApp.Themes.Extensions
{
    public static class WindowExtension
    {
        public static bool? GetDialogResult(DependencyObject obj)
        {
            return (bool?)obj.GetValue(DialogResultProperty);
        }

        public static void SetDialogResult(DependencyObject obj, bool? value)
        {
            obj.SetValue(DialogResultProperty, value);
        }

        // Using a DependencyProperty as the backing store for DialogResult.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.RegisterAttached("DialogResult", typeof(bool?), typeof(WindowExtension), new PropertyMetadata(default, (d, e) =>
             {
                 if (d is Window handler)
                 {
                     handler.DialogResult = e.NewValue as bool?;
                 }
             }));

    }
}
