using System.Globalization;
using System.Windows.Data;

namespace PresentationLayer.WPF.Helpers
{
    public class MenuSelectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            return value.ToString() == parameter.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // When the user clicks the ToggleButton, we update SelectedMenu
            if ((bool)value)
                return parameter.ToString();
            else
                return Binding.DoNothing;
        }
    }
}
