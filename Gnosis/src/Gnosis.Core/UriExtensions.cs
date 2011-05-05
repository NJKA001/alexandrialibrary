using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Gnosis.Core
{
    public static class UriExtensions
    {
        private static string GetMd5Hash(Uri url)
        {
            var request = HttpWebRequest.Create(url);
            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    return stream.AsMd5Hash();
                }
            }
        }

        private static string GetMd5Hash(string fileName)
        {
            using (var file = new FileStream(fileName, FileMode.Open))
            {
                return file.AsMd5Hash();
            }
        }

        public static string AsMd5Hash(this Uri location)
        {
            try
            {
                if (location.IsFile)
                {
                    return GetMd5Hash(location.LocalPath);
                }
                else
                {
                    return GetMd5Hash(location);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
