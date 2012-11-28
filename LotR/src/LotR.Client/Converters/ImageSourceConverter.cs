using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LotR.Client.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(ImageSource))
                throw new InvalidOperationException("The target must be an ImageSource or a derived type");

            const string placeholder = "pack://application:,,,/Images/player_back.png";

            if (value == null)
                return placeholder;

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

            if (value is Image)
            {
                var originalImage = value as Image;
                var stream = new MemoryStream();
                originalImage.Save(stream, ImageFormat.Png);
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
