using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;

using LotR.Cards.Player;
using LotR.States;

namespace LotR.Client.ViewModels
{
    public class PlayerCardInPlayViewModel
        : PlayerCardViewModel
    {
        public PlayerCardInPlayViewModel(Dispatcher dispatcher, IPlayerCardInPlay cardInPlay)
            : base(dispatcher, cardInPlay.PlayerCard)
        {
            this.cardInPlay = cardInPlay;
            cardInPlay.PropertyChanged += (sender, args) => CardInPlayPropertyChanged(sender, args);
        }

        private readonly IPlayerCardInPlay cardInPlay;

        private void CardInPlayPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "Resources":
                    OnPropertyChanged("Resources");
                    break;
                case "Damage":
                    OnPropertyChanged("Damage");
                    break;
                case "Progress":
                    OnPropertyChanged("Progress");
                    break;
                default:
                    break;
            }
        }

        public IPlayerCardInPlay CardInPlay
        {
            get { return cardInPlay; }
        }

        public byte Resources
        {
            get { return cardInPlay.Resources; }
        }

        public byte Damage
        {
            get { return cardInPlay.Damage; }
        }

        public byte Progress
        {
            get { return cardInPlay.Progress; }
        }
    }

    public class PlayerCardInPlayViewModel<T>
        : PlayerCardInPlayViewModel
        where T : IPlayerCard
    {
        public PlayerCardInPlayViewModel(Dispatcher dispatcher, IPlayerCardInPlay<T> cardInPlay)
            : base(dispatcher, cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            this.cardInPlay = cardInPlay;
        }

        private readonly IPlayerCardInPlay<T> cardInPlay;
    }
}
