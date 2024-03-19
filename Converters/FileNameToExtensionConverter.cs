using System;
using System.IO;
using System.Globalization;
using System.Windows.Data;

namespace WindowsVirusScanningSystem.Converters
{
    public class FileNameToExtensionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string name ? Path.GetExtension(name) : "Folder";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
