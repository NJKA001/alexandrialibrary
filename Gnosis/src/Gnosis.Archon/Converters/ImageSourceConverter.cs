using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;

namespace Gnosis.Archon.Converters
{
    //reference: - http://cromwellhaus.com/blogs/ryanc/archive/2007/07/26/binding-to-the-byte-of-an-image-in-wpf.aspx
    internal class ImageSourceConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(ImageSource))
                throw new InvalidOperationException("The target must be ImageSource or derived types");

            const string placeholder = "pack://application:,,,/Images/placeholder.jpg";

            if (value == null)
                return placeholder;

            //if (value != null && value is List<Byte>)
            //{
            //    List<Byte> bytes = value as List<Byte>;
            //    if (bytes.Count > 0)
            //    {
            //        MemoryStream stream = new MemoryStream(bytes.ToArray());
            //        BitmapImage image = new BitmapImage();
            //        image.BeginInit();
            //        image.StreamSource = stream;
            //        image.EndInit();
            //        return image;
            //    }
            //}

            if (value is ICollection<byte>)
            {
                var bytes = value as ICollection<byte>;
                if (bytes.Count > 0)
                {
                    try
                    {
                        var buffer = new byte[bytes.Count];
                        bytes.CopyTo(buffer, 0);
                        var stream = new MemoryStream(buffer);
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.StreamSource = stream;
                        image.EndInit();
                        return image;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }

            if (value is string)
            {
                var path = value as string;

                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.CreateOptions = BitmapCreateOptions.None;
                image.UriSource = new Uri(path, UriKind.Absolute);
                image.EndInit();
                return image;
            }

            return placeholder;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
