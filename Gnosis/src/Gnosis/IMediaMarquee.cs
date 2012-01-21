using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMediaMarquee
    {
        Uri Location { get; }
        MediaCategory Category { get; }
        string Name { get; }
        string Subtitle { get; }
    }
}
