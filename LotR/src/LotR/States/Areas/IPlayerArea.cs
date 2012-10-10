using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;

namespace LotR.States.Areas
{
    public interface IPlayerArea
        : IArea
    {
        IPlayerDeck PlayerDeck { get; }
        IEnumerable<IAttachableCard> PlayerDeckAttachments { get; }

        ICardInPlay<IPlayerCard> GetPlayerCard(Guid id);
    }
}
