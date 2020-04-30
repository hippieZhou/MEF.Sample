using BlackApp.Application.Modularity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace BlankApp.Converters
{
    public class ModuleToHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is IEnumerable<BusinessModuleEntity> modules
                ? modules.OrderBy(x => x.Priority).GroupBy(x => x.Header).Where(x => !string.IsNullOrWhiteSpace(x.Key)).Select(x => x.Key)
                : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
