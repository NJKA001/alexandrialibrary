using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Tags.Id3
{
    public static class Id3Schemas
    {
        public static readonly ITagSchema Id3v1Schema = new TagSchema(2, "ID3v1");
        public static readonly ITagSchema Id3v2Schema = new TagSchema(3, "ID3v2");
    }
}
