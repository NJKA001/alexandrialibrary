using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

namespace LotR.States.Phases.Any
{
    public class CardEntersPlay
        : StateBase, ICardEntersPlay
    {
        public CardEntersPlay(IGame game, ICardInPlay enteringPlay)
            : base(game)
        {
            this.EnteringPlay = enteringPlay;
        }

        private bool isEnteringPlay = true;

        public ICardInPlay EnteringPlay
        {
            get;
            private set;
        }

        public bool IsEnteringPlay
        {
            get { return isEnteringPlay; }
            set
            {
                if (isEnteringPlay == value)
                    return;

                isEnteringPlay = value;
                OnPropertyChanged("IsEnteringPlay");
            }
        }
    }
}
