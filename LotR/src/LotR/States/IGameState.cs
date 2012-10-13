using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects;

namespace LotR.States
{
    public interface IGameState
        : IState
    {
        Phase CurrentPhase { get; }

        void AddEffect(IEffect effect);

        bool CardHasTrait(ICard card, Trait trait);
        bool CardInPlayHasTrait(ICardInPlay card, Trait trait);

        IPlayer ActivePlayer { get; }
        IPlayer FirstPlayer { get; }
    }
}
