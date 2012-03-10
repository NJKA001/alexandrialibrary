using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IMovie
        : IEntity
    {
        string Name { get; set; }
        ushort Year { get; set; }
    }
}
