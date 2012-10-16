using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Phases.Any;

namespace LotR.States
{
    public interface ICardInPlay
        : IState, IDuringCheckForResourceIcon, IDuringCheckForTrait
    {
        string Title { get; }

        IPlayer GetController(IGameState state);
    }

    public interface ICardInPlay<T>
        : ICardInPlay
        where T : ICard
    {
        T Card { get; }
    }
}
