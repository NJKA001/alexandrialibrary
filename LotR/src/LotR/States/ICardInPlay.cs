using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects;
using LotR.Effects.Phases.Any;

namespace LotR.States
{
    public interface ICardInPlay
        : IState
    {
        string Title { get; }

        IPlayer GetController(IGame game);

        bool HasEffect<T>() where T : IEffect;
        bool HasTrait(Trait trait);
    }

    public interface ICardInPlay<T>
        : ICardInPlay
        where T : ICard
    {
        T Card { get; }
    }
}
