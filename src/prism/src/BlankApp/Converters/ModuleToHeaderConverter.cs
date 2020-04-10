using BlankApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace BlankApp.Converters
{
    public class ModuleToHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is IEnumerable<BusinessViewModel> modules
                ? modules.GetHeaders()
                : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
