using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public static class StringArrayExtensions
    {
        public static TagTuple ToTagTuple(this string[] self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            switch (self.Length)
            {
                case 0:
                    return new TagTuple(string.Empty);
                case 1:
                    return new TagTuple(self[0]);
                case 2:
                    return new TagTuple(self[0], self[1]);
                case 3:
                    return new TagTuple(self[0], self[1], self[2]);
                case 4:
                    return new TagTuple(self[0], self[1], self[2], self[3]);
                case 5:
                    return new TagTuple(self[0], self[1], self[2], self[3], self[4]);
                case 6:
                    return new TagTuple(self[0], self[1], self[2], self[3], self[4], self[5]);
                case 7:
                default:
                    return new TagTuple(self[0], self[1], self[2], self[3], self[4], self[5], self[6]);
            }
        }
    }
}
