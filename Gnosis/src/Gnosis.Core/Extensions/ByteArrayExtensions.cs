using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public static class ByteArrayExtensions
    {
        public static System.Drawing.Image ToImage(this byte[] self)
        {
            var stream = new MemoryStream(self);
            return System.Drawing.Image.FromStream(stream);
        }
    }
}
