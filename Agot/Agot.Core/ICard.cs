using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface ICard
    {
        string Title { get; }
        CardType Type { get; }
        CardSet Set { get; }
        IText Text { get; }
    }
}
