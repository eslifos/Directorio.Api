using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Directorio.WpfClient.Converters
{
    public class WidthToVisibilityConverter : IValueConverter
    {
        private const double Breakpoint = 600.0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not double actualWidth || parameter is not string targetView)
            {
                return Visibility.Collapsed;
            }
                 
            if (targetView == "Desktop")
            {            
                return actualWidth >= Breakpoint ? Visibility.Visible : Visibility.Collapsed;
            }
                        
            if (targetView == "Mobile")
            {             
                return actualWidth < Breakpoint ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}