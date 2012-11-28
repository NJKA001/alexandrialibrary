using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.Client.ViewModels
{
    public class PlayerCardViewModel
        : ViewModelBase
    {
        public PlayerCardViewModel(IPlayerCard card)
        {
            if (card == null)
                throw new ArgumentNullException("card");

            this.card = card;
            this.cardFrontImageSource = string.Format("pack://application:,,,/Images/{0}_{1}.jpg", card.CardSet, card.CardNumber);
        }

        private readonly IPlayerCard card;
        private readonly string cardFrontImageSource;
        private const string cardBackImageSource = "pack://application:,,,/Images/player_back.png";
        private bool isFaceUp = true;

        public string Title
        {
            get { return card.Title; }
        }

        public object ImageSource
        {
            get { return isFaceUp ? cardFrontImageSource : cardBackImageSource; }
        }

        public bool IsFaceUp
        {
            get { return isFaceUp; }
            set
            {
                if (isFaceUp == value)
                    return;

                isFaceUp = value;
                OnPropertyChanged("IsFaceUp");
                OnPropertyChanged("ImageSource");
            }
        }
    }
}
