using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Cards.Player.Heroes;

namespace LotR.Games
{
    public interface IPlayerDeck
        : IDeck<IPlayerCard>
    {
        string Name { get; }
        IEnumerable<IHeroCard> Heroes { get; }
    }
}
