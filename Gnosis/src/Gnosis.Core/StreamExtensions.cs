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

        public static string AsMd5Hash(this Stream stream)
        {
            var md5 = new MD5CryptoServiceProvider();
            var retVal = md5.ComputeHash(stream);
            
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public static void Copy(this Stream source, Stream target, int blockSize)
        {
            int read;
            byte[] buffer = new byte[blockSize];
            while ((read = source.Read(buffer, 0, blockSize)) > 0)
            {
                target.Write(buffer, 0, read);
            }
        }

        public static void BlockCopy(this Stream source, Stream target, int blockSize = 65536)
        {
            source.Copy(target, blockSize);
        }

        public static void SaveToFile(this Stream source, string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                source.BlockCopy(fs);
                fs.Flush();
                fs.Close();
            }
        }
    }
}
