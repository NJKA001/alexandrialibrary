using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Phases.Any
{
    public class CheckForTraitStep
        : PhaseStepBase, ICheckForTraitStep
    {
        public CheckForTraitStep(IPhase phase, IPlayer player, ICardInPlay cardInPlay, Trait trait)
            : base(phase, player)
        {
            this.CardInPlay = cardInPlay;
            this.Trait = trait;
        }

        public ICardInPlay CardInPlay
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
