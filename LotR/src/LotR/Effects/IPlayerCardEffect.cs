using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.Effects
{
    public interface IPlayerCardEffect
        : ICardEffect
    {
        new IPlayerCard Source { get; }
    }
}
