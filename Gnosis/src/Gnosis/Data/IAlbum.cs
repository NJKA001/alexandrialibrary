using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IAlbum
        : IEntity
    {
        string Name { get; set; }
        uint Artist { get; set; }
        ushort Year { get; set; }
        ushort Number { get; set; }
    }
}
