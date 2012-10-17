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
        protected PlayerCardInPlay(IGame game, T card, IPlayer owner)
            : base(game, card)
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
