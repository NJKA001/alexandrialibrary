using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects
{
    public interface ICardEffect
        : IEffect
    {
        ISource Source { get; }
    }
}
