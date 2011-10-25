using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Gnosis
{
    public static class ByteArrayExtensions
    {
        public static System.Drawing.Image ToImage(this byte[] self)
        {
            var stream = new MemoryStream(self);
            return System.Drawing.Image.FromStream(stream);
        }

        public static string ToHexString(this byte[] self)
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

        public static string ToMd5Hash(this byte[] self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var md5 = new MD5CryptoServiceProvider();
            var bytes = md5.ComputeHash(self);

            return bytes.ToHexString();
        }

        public static string ToSha1Hash(this byte[] self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var sha1 = new SHA1CryptoServiceProvider();
            var bytes = sha1.ComputeHash(self);

            return bytes.ToHexString();
        }
    }
}
