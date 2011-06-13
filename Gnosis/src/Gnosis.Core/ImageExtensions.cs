using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public static class ImageExtensions
    {
        public static byte[] ToBytes(this Image image)
        {
            var stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            return stream.ToArray();
        }

        public static Image ToImage(this byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            return Image.FromStream(stream);
        }
    }
}
