using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gnosis.Extensions
{
    public static class FileInfoExtensions
    {
        public static byte[] ToHeader(this FileInfo self)
        {
            return self.ToHeader(20);
        }

        public static byte[] ToHeader(this FileInfo self, int headerSize)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (headerSize <= 0)
                throw new ArgumentException("headerSize must be greater than zero");
            if (!self.Exists || self.Length < headerSize)
                return new byte[headerSize];
                //throw new ArgumentException("file must exist and have a length greater than headerSize");

            var header = new byte[headerSize];

            try
            {
                using (var fileStream = new System.IO.FileStream(self.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    fileStream.Read(header, 0, headerSize);
                }
                return header;
            }
            catch
            {
                return new byte[headerSize];
            }
        }
    }
}
