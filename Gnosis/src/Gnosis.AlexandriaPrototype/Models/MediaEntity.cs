using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;

namespace Gnosis.Alexandria.Models
{
    public class MediaEntity : Model, IMediaEntity
    {
        public string Type { get; set; }

        public string Marquee { get; set; }

        public string Hash { get; set; }

        public string By { get; set; }

        public string On { get; set; }

        public string From { get; set; }
    }
}
