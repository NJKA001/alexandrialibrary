using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Tags.Id3
{
    public class Id3v2Schema
        : Schema
    {
        public Id3v2Schema()
            : base(Id3v2.ToUri(), "ID3v2")
        {
        }

        public const string Id3v2 = "http://gn0s1s.com/ns/1/tag-types/id3/v2/";
        public const string Id3v2Artist = "http://gn0s1s.com/ns/1/tag-types/id3/v2/artist";
        public const string Id3v2Title = "http://gn0s1s.com/ns/1/tag-types/id3/v2/album";
    }
}
