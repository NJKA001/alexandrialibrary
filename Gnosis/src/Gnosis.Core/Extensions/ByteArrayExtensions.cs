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

        public static string ToMd5Hash(this byte[] self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var builder = new StringBuilder();
            for (int i = 0; i < self.Length; i++)
            {
                builder.Append(self[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
