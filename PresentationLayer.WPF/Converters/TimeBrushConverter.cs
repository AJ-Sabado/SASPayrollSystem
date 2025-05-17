using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace PresentationLayer.WPF.Converters
{
    public class TimeBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Brushes.DarkGreen : Brushes.DarkRed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
