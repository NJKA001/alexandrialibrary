using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public static class GuidExtensions
    {
        public static Uri ToUrn(this Guid self)
        {
            return new Uri(string.Format("urn:uuid:{0}", self.ToString()));
        }
    }
}
