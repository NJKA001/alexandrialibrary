using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace Gnosis.Alexandria.Converters
{
    /// <summary>
    /// Convert width and height values into a Rect
    /// </summary>
    /// <remarks>
    /// See http://stackoverflow.com/questions/5167867/progress-bar-with-dynamic-text-text-color-update
    /// </remarks>
    internal class RectConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (values[0] is int)
                {
                    int width = (int)values[0];
                    int height = (int)values[1];
                    return new Rect(0, 0, width, height);
                }
                if (values[0] is double)
                {
                    double width = (double)values[0];
                    double height = (double)values[1];
                    return new Rect(0, 0, width, height);
                }
            }
            catch (Exception)
            {
            }

            return new Rect(0, 0, 0, 0);
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
