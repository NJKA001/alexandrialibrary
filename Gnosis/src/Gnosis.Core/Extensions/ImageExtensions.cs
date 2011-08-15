using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public static class ImageExtensions
    {
        public static byte[] ToBytes(this Image self)
        {
            return self.ToBytes(ImageFormat.Bmp);
        }

        public static byte[] ToBytes(this Image self, ImageFormat format)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var stream = new MemoryStream();
            self.Save(stream, format);
            return stream.ToArray();
        }
    }
}
