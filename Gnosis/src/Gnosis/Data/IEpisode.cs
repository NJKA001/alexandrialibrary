using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IEpisode
        : IEntity
    {
        string Name { get; set; }
        uint Season { get; set; }
        ushort Number { get; set; }
        ushort Duration { get; set; }
    }
}
