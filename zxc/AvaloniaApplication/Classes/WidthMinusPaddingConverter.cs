using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication.Classes
{
    public class WidthMinusPaddingConverter : IValueConverter
    {
        private const double SomePadding = 5; // Значение отступов.
        private const double WidthOfButtons = 30; // Общая ширина кнопок.

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double availableWidth = (double)value;
            // Уменьшите доступную ширину на значение отступов и ширину кнопок.
            return availableWidth - (2 * SomePadding + WidthOfButtons);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
