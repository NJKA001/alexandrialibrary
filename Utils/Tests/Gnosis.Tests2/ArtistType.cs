using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    [Flags]
    public enum ArtistType : ushort
    {
        None = 0,
        Musician = 1,
        Singer = 2,
        Songwriter = 4,
        Composer = 8,
        Writer = 16,
        Actor = 32,
        Painter = 64,
        Sculptor = 128,
        Director = 256
    }
}
