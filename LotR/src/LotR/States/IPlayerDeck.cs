using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Heroes;

namespace LotR.States
{
    public interface IPlayerDeck
        : IDeck<IPlayerCard>, ICanHaveAttachments
    {
        string Name { get; }
        byte Threat { get; }
        IEnumerable<IHeroCard> Heroes { get; }
    }
}
