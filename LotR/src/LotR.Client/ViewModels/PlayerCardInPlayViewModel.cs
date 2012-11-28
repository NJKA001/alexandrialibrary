using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.States;

namespace LotR.Client.ViewModels
{
    public class PlayerCardInPlayViewModel<T>
        : PlayerCardViewModel
        where T : IPlayerCard
    {
        public PlayerCardInPlayViewModel(IPlayerCardInPlay<T> cardInPlay)
            : base(cardInPlay.Card)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            this.cardInPlay = cardInPlay;
        }

        private readonly IPlayerCardInPlay<T> cardInPlay;

        public byte Resources
        {
            get { return cardInPlay.Resources; }
        }

        public byte Damage
        {
            get { return cardInPlay.Damage; }
        }
    }
}
