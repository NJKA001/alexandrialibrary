using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface ICardInHand
    {
        bool IsInHouse { get; }
        ICard Card { get; }
    }
}
