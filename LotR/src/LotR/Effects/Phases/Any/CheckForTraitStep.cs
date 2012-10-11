using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public class CheckForTraitStep
        : PhaseStepBase, ICheckForTraitStep
    {
        public CheckForTraitStep(IPhase phase, IPlayer player, ICardInPlay<ICard> cardInPlay, Trait trait)
            : base(phase, player)
        {
            this.CardInPlay = cardInPlay;
            this.Trait = trait;
        }

        public ICardInPlay<ICard> CardInPlay
        {
            get;
            private set;
        }

        public Trait Trait
        {
            get;
            private set;
        }

        public bool HasTrait
        {
            get;
            set;
        }
    }
}
