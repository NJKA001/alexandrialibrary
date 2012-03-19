using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMarquee
    {
        Uri Location { get; }
        MetadataCategory Category { get; }
        string Name { get; }
        string Subtitle { get; }
    }
}
