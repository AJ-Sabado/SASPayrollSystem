using System.Globalization;
using System.Windows.Data;

namespace PresentationLayer.WPF.Converters
{
    public class TimeIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isTimedIn = (bool)value;
            return isTimedIn ? "DoorOpen" : "DoorClosed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
