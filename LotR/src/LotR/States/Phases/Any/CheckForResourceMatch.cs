using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Costs;

namespace LotR.States.Phases.Any
{
    public class CheckForResourceMatch
        : StateBase, ICheckForResourceMatch
    {
        public CheckForResourceMatch(IGame game, ICard card, ICost cost)
            : base(game)
        {
            this.Card = card;
            this.Cost = cost;
        }

        private bool isResourceMatch;

        public ICard Card
        {
            get;
            private set;
        }

        public ICost Cost
        {
            get;
            private set;
        }

        public bool IsResourceMatch
        {
            get { return isResourceMatch; }
            set
            {
                if (isResourceMatch == value)
                    return;

                isResourceMatch = value;
                OnPropertyChanged("IsResourceMatch");
            }
        }
    }
}
