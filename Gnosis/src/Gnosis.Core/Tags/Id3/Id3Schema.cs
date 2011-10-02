using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Tags.Id3
{
    public class Id3Schema
        : Schema
    {
        public Id3Schema()
            : base(new Uri(Id3), "ID3")
        {

        }

        public const string Id3 = "http://gn0s1s.com/ns/1/tag-types/id3/";
    }
}
