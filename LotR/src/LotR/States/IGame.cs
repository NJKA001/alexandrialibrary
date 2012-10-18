using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects;
using LotR.States.Areas;

namespace LotR.States
{
    public interface IGame
        : IState
    {
        Phase CurrentPhase { get; }
        PhaseStep CurrentPhaseStep { get; }

        void AddEffect(IEffect effect);

        bool CardHasTrait(ICard card, Trait trait);
        bool CardInPlayHasTrait(ICardInPlay card, Trait trait);

        IEnumerable<IPlayer> Players { get; }
        IPlayer ActivePlayer { get; }
        IPlayer FirstPlayer { get; }

        void Setup(IQuestArea questArea, IEnumerable<IPlayer> players);
    }
}
