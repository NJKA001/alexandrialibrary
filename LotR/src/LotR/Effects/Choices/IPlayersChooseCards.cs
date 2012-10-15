using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects.Choices
{
    public interface IPlayersChooseCards
        : IChoice
    {
        byte NumberOfCards { get; }

        IEnumerable<ICard> GetAvailableCards(Guid playerId);
        IEnumerable<ICard> GetChosenCards(Guid playerId);
        void AddChosenCard(Guid playerId, ICard card);
    }
}
