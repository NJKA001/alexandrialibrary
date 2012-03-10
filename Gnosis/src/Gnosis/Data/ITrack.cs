using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface ITrack
        : IEntity
    {
        string Name { get; set; }
        uint Artist { get; set; }
        uint Album { get; set; }
        byte Disc { get; set; }
        byte Number { get; set; }
        ushort Duration { get; set; }
    }
}
