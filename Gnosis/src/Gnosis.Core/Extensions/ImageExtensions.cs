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
        public static byte[] ToBytes(this System.Drawing.Image self)
        {
            return self.ToBytes(System.Drawing.Imaging.ImageFormat.Bmp);
        }

        public static byte[] ToBytes(this System.Drawing.Image self, System.Drawing.Imaging.ImageFormat format)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var stream = new MemoryStream();
            self.Save(stream, format);
            return stream.ToArray();
        }
    }
}
