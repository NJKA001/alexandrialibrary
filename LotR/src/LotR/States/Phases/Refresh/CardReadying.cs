using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Refresh
{
    public class CardReadying
        : StateBase, ICardReadying
    {
        public CardReadying(IGame game, IExhaustableInPlay exhaustable)
            : base(game)
        {
        }

        private bool isReadying;

        public IExhaustableInPlay Exhaustable
        {
            get;
            private set;
        }

        public bool IsReadying
        {
            get { return isReadying; }
            set
            {
                if (isReadying == value)
                    return;

                isReadying = value;
                OnPropertyChanged("IsReadying");
            }
        }
    }
}
