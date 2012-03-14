using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class ArtistLink
    {
        Artist Artist { get; set; }
        public string Name { get; set; }
        public Relationship Relationship { get; set; }
        public string Target { get; set; }
    }
}
