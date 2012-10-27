using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public interface IPlayersDrawingCards
        : IState
    {
        IEnumerable<Guid> Players { get; }

        byte GetNumberOfCards(Guid playerId);
        bool PlayerCanDrawCards(Guid playerId);

        void SetNumberOfCards(Guid playerId, byte numberOfCards);
        void EnabledPlayerCardDraw(Guid playerId);
        void DisablePlayerCardDraw(Guid playerId);
    }
}
