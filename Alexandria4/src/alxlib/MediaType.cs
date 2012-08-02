using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public class MediaType
        : IMediaType
    {
        public MediaSupertype Supertype
        {
            get;
            set;
        }

        public string Subtype
        {
            get;
            set;
        }

        public IEnumerable<string> FileExtensions
        {
            get;
            set;
        }

        public IEnumerable<byte[]> MagicNumbers
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", Enum.GetName(typeof(MediaSupertype), Supertype), Subtype);
        }
    }
}
