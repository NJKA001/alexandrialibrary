using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Extensions
{
    public static class UriExtensions
    {
        private const string emptyUrn = "urn:empty";

        public static readonly Uri EmptyUri = new Uri(emptyUrn);

        public static bool IsEmpty(this Uri self)
        {
            if (self == null)
                return false;

            return self.AbsolutePath == emptyUrn;
        }
    }
}
