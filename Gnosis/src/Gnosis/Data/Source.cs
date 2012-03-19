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
        System_DoubleMetaphone = 201,
        System_Americanized = 202,
        
        Embedded = 400,
        Embedded_Id3v1 = 401,
        Embedded_Id3v2 = 402,
        
        Service = 800,
        Service_LastFm = 801
    }
}
