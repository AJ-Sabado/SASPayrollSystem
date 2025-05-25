using System;
using System.Globalization;
using System.Windows.Data;

namespace PresentationLayer.WPF.Helpers
{
    public class MenuSelectionConverter : IValueConverter
    {
        // Convert from SelectedMenu (string) to IsChecked (bool)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return false;
            return value.ToString() == parameter.ToString();
        }

        // Convert back from IsChecked (bool) to SelectedMenu (string)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isChecked = (bool)value;

            // Only update SelectedMenu when a button is being selected (true)
            if (isChecked)
                return parameter.ToString();
            else
                return Binding.DoNothing; // Prevent unchecking when same button is clicked
        }
    }
}
