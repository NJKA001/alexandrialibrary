using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IFeedItem
        : IEntity
    {
        string Name { get; set; }
        uint Feed { get; set; }
        long Updated { get; set; }
        ushort Published { get; set; }
        ushort Number { get; set; }
    }
}
