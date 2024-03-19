using System;
using System.IO;
using System.Globalization;
using System.Windows.Data;

namespace WindowsVirusScanningSystem.Converters
{
    /// <summary>
    /// Used to get the filename with extention.
    /// </summary>
    public sealed class PathToFilenameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (value is string filename)
                ? Path.GetFileName(filename)
                : string.Empty;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
    }
}
