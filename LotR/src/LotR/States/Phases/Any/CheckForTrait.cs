using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States.Phases.Any
{
    public class CheckForTrait
        : StateBase, ICheckForTrait
    {
        public CheckForTrait(IGame game, ICardInPlay target, Trait trait)
            : base(game)
        {
            this.Target = target;
            this.Trait = trait;
        }

        private bool hasTrait;

        public ICardInPlay Target
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
            get { return hasTrait; }
            set
            {
                if (hasTrait != value)
                {
                    hasTrait = value;
                    OnPropertyChanged("HasTrait");
                }
            }
        }
    }
}
