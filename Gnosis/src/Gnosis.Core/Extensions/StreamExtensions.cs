using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Gnosis.Core
{
    public static class StreamExtensions
    {
        public static void Copy(this Stream self, Stream target)
        {
            self.Copy(target, 65536);
        }

        public static void Copy(this Stream self, Stream target, int blockSize)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (target == null)
                throw new ArgumentNullException("target");
            if (blockSize <= 0)
                throw new ArgumentException("blockSize must be greater than zero");

            int read;
            byte[] buffer = new byte[blockSize];
            while ((read = self.Read(buffer, 0, blockSize)) > 0)
            {
                target.Write(buffer, 0, read);
            }
        }

        public static void SaveToFile(this Stream self, string fileName)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (fileName == null)
                throw new ArgumentNullException("fileName");

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                self.Copy(fs);
                fs.Flush();
                fs.Close();
            }
        }

        public static byte[] ToBuffer(this Stream self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var memoryStream = new MemoryStream();
            self.CopyTo(memoryStream);
            return memoryStream.GetBuffer();
        }

        public static string ToContentString(this Stream self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            using (var reader = new StreamReader(self))
            {
                return reader.ReadToEnd();
            }
        }

        public static byte[] ToHeader(this Stream self)
        {
            return self.ToHeader(20);
        }

        public static byte[] ToHeader(this Stream self, int headerSize)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (headerSize <= 0)
                throw new ArgumentException("headerSize must be greater than zero");

            var header = new byte[headerSize];

            self.Read(header, 0, headerSize);

            return header;
        }

        public static string ToMd5Hash(this Stream self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var md5 = new MD5CryptoServiceProvider();
            var bytes = md5.ComputeHash(self);

            return bytes.ToMd5Hash();
        }
    }
}
