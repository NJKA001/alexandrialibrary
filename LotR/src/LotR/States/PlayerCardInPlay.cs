using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;

namespace LotR.States
{
    public abstract class PlayerCardInPlay<T>
        : CardInPlay<T>, 
        IPlayerCardInPlay<T>,
        IExhaustableInPlay
        where T : IPlayerCard
    {
        protected PlayerCardInPlay(IGame game, T card)
            : base(game, card)
        {
        }

        private bool isExhausted;

        public IPlayerCard PlayerCard
        {
            get { return Card; }
        }

        public bool IsExhausted
        {
            get { return isExhausted; }
        }

        public void Exhaust()
        {
            if (isExhausted)
                return;

            isExhausted = true;
            OnPropertyChanged("IsExhausted");
        }

        public void Ready()
        {
            if (!isExhausted)
                return;

            isExhausted = false;
            OnPropertyChanged("IsExhausted");
        }
    }
}
