using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public static class FileInfoExtensions
    {
        public static byte[] GetHeader(this FileInfo self)
        {
            const int headerSize = 20;

            if (self == null || !self.Exists || self.Length < headerSize)
                return null;

            var header = new byte[headerSize];

            using (var fileStream = new System.IO.FileStream(self.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                fileStream.Read(header, 0, headerSize);
            }

            return header;
        }
    }
}
