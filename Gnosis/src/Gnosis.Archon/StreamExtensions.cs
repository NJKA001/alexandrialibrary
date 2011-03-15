using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gnosis.Archon
{
    public static class StreamExtensions
    {
        //public static void CopyTo(this Stream input, Stream output)
        //{
        //    byte[] buffer = new byte[32768];
        //    var read = 0;
        //    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
        //        output.Write(buffer, 0, read);
        //}

        public static byte[] AsBuffer(this Stream input)
        {
            var memoryStream = new MemoryStream();
            input.CopyTo(memoryStream);
            return memoryStream.GetBuffer();
        }
    }
}
