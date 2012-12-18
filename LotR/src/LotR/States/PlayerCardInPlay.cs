using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.States
{
    public abstract class PlayerCardInPlay<T>
        : CardInPlay<T>, IPlayerCardInPlay<T>
        where T : IPlayerCard
    {
        protected PlayerCardInPlay(IGame game, T card)
            : base(game, card)
        {
        }

        public IPlayerCard PlayerCard
        {
            get { return Card; }
        }
    }
}
