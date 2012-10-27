using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public class CardLeavesPlay
        : StateBase, ICardLeavesPlay
    {
        public CardLeavesPlay(IGame game, ICardInPlay leavingPlay)
            : base(game)
        {
            this.LeavingPlay = leavingPlay;
        }

        private bool isLeavingPlay = true;

        public ICardInPlay LeavingPlay
        {
            get;
            private set;
        }

        public bool IsLeavingPlay
        {
            get { return isLeavingPlay; }
            set
            {
                if (isLeavingPlay == value)
                    return;

                isLeavingPlay = value;
                OnPropertyChanged("IsLeavingPlay");
            }
        }
    }
}
