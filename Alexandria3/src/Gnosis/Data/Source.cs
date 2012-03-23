using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public enum Source : ushort
    {
        None = 0,
        
        User = 100,
        
        System = 200,
        DoubleMetaphone = 201,
        Americanized = 202,
        
        Embedded = 400,
        Id3v1 = 401,
        Id3v2 = 402,
        Riff = 403,

        Service = 800,
        MusicBrainz = 801,
        LastFm = 802
    }
}
