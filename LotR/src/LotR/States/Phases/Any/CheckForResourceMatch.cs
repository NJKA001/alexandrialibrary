using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.States.Phases.Any
{
    public class CheckForResourceMatch
        : StateBase, ICheckForResourceMatch
    {
        public CheckForResourceMatch(IGame game, ICostlyCard costlyCard)
            : base(game)
        {
            this.CostlyCard = costlyCard;
        }

        private bool isResourceMatch;

        public ICostlyCard CostlyCard
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
