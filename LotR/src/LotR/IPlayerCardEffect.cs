using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IPlayerCardEffect
        : ICardEffect
    {
        new IPlayerCard Source { get; }
    }
}
