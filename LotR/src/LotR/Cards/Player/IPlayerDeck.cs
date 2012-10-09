using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;

namespace LotR.Cards.Player
{
    public interface IPlayerDeck
        : IDeck<IPlayerCard>
    {
        string Name { get; }
        byte Threat { get; }
        IEnumerable<IHeroCard> Heroes { get; }
    }
}
