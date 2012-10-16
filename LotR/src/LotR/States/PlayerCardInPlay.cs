using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.States
{
    public abstract class PlayerCardInPlay<T>
        : CardInPlay<T>
        where T : IPlayerCard
    {
        protected PlayerCardInPlay(T card, IPlayer owner)
            : base(card)
        {
            this.Owner = owner;
        }

        public IPlayer Owner
        {
            get;
            private set;
        }
    }
}
