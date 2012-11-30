using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

using LotR.Cards;
using LotR.Cards.Player;

namespace LotR.Client.ViewModels
{
    public class PlayerCardViewModel
        : ViewModelBase
    {
        public PlayerCardViewModel(Dispatcher dispatcher, IPlayerCard card)
            : base(dispatcher)
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

        public Guid CardId
        {
            get { return card.Id; }
        }

        public string Title
        {
            get { return card.Title; }
        }

        public object ImageSource
        {
            get { return isFaceUp ? cardFrontImageSource : cardBackImageSource; }
        }

        public object ResourceIcon
        {
            get
            {
                switch (card.PrintedSphere)
                {
                    case Sphere.Leadership:
                        return "pack://application:,,,/Images/leadership_small.png";
                    case Sphere.Lore:
                        return "pack://application:,,,/Images/lore_small.png";
                    case Sphere.Spirit:
                        return "pack://application:,,,/Images/spirit_small.png";
                    case Sphere.Tactics:
                        return "pack://application:,,,/Images/tactics_small.png";
                    case Sphere.Neutral:
                    default:
                        return null;
                }
            }
        }

        public string CardText
        {
            get { return card.Text.ToString(); }
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
