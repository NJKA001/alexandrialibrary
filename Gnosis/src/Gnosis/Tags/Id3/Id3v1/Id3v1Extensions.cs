using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tags.Id3.Id3v1
{
    public static class Id3v1Extensions
    {
        public static string ToFriendlyString(this Id3v1Genre self)
        {
            return self.ToString().Replace("_and_", "&").Replace("_plus_", "+").Replace("_slash_", "/").Replace("_dash_", "-").Replace("_", "_");
        }
    }
}
