using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface ICard
    {
        string Name { get; }
        CardType Type { get; }
        House HouseRestriction { get; }
    }
}
