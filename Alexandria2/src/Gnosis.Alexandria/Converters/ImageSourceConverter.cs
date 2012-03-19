using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;

namespace Gnosis.Alexandria.Converters
{
    /// <summary>
    /// Convert various types into an ImageSource
    /// </summary>
    /// <remarks>
    /// See http://cromwellhaus.com/blogs/ryanc/archive/2007/07/26/binding-to-the-byte-of-an-image-in-wpf.aspx
    /// </remarks>
    internal class ImageSourceConverter : IValueConverter
    {
        private const string placeholder = "pack://application:,,,/Images/placeholder.jpg";

        private object GetImage(string path)
        {
            try
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.CreateOptions = BitmapCreateOptions.None;
                image.UriSource = new Uri(path, UriKind.Absolute);
                image.EndInit();
                return image;
            }
            catch (Exception)
            {
                return placeholder;
            }
        }

        private object GetImage(byte[] data)
        {
            try
            {
                var stream = new MemoryStream(data);
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();
                return image;
            }
            catch (Exception)
            {
                return placeholder;
            }
        }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(ImageSource))
                throw new InvalidOperationException("The target must be ImageSource or derived types");

            if (value == null)
                return placeholder;

            try
            {
                if (value is ImageSource)
                {
                    return value as ImageSource;
                }

                if (value is string || value is Uri)
                {
                    var path = value.ToString();

                    if (path.EndsWith(".svg"))
                    {
                        return placeholder;
                    }
                    else
                    {
                        return GetImage(path);
                    }
                }

                if (value is IImage)
                {
                    var image = value as IImage;
                    if (image.IsLoaded)
                        return GetImage(image.GetData());
                    else
                        return GetImage(image.Location.ToString());
                }

                if (value is byte[])
                {
                    return GetImage((byte[])value);
                }

                if (value is IEnumerable<byte>)
                {
                    return GetImage(((IEnumerable<byte>)value).ToArray());
                }

                if (value is Bitmap)
                {
                    var bitmap = value as Bitmap;
                    var stream = new MemoryStream();
                    bitmap.Save(stream, ImageFormat.Png);
                    stream.Position = 0;
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.EndInit();

                    return image;
                }

                if (value is System.Windows.Controls.Image)
                {
                    var imageControl = value as System.Windows.Controls.Image;
                    if (imageControl != null)
                        return imageControl.Source;
                }

                if (value is System.Drawing.Image)
                {
                    var originalImage = value as System.Drawing.Image;
                    var stream = new MemoryStream();
                    originalImage.Save(stream, ImageFormat.Bmp);
                    stream.Position = 0;
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.EndInit();

                    return image;
                }

                if (value is Icon)
                {
                    var icon = value as Icon;
                    var stream = new MemoryStream();
                    icon.Save(stream);
                    stream.Position = 0;
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.EndInit();

                    return image;
                }

                return placeholder;
            }
            catch (Exception)
            {
                return placeholder;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
