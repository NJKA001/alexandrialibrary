using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class ArtistTag
    {
        Artist Artist { get; set; }
        string Name { get; set; }
        Relationship Relationship { get; set; }
        string Target { get; set; }
        Category Category { get; set; }
        Source Source { get; set; }
    }
}
