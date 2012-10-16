using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects.Choices
{
    public interface IPlayersChooseCards<T>
        : IChoice
        where T : ICard
    {
        byte NumberOfCards { get; }

        IEnumerable<T> GetAvailableCards(Guid playerId);
        IEnumerable<T> GetChosenCards(Guid playerId);
        bool ChosenCardIsValid(Guid playerId, T card);

        void AddChosenCard(Guid playerId, T card);
    }
}
