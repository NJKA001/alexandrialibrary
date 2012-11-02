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

        uint GetNumberOfCards(Guid playerId);
        bool PlayerCanDrawCards(Guid playerId);

        void SetNumberOfCards(Guid playerId, uint numberOfCards);
        void EnabledPlayerCardDraw(Guid playerId);
        void DisablePlayerCardDraw(Guid playerId);
    }
}
