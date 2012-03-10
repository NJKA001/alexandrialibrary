using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IShow
        : IEntity
    {
        string Name { get; set; }
    }
}
