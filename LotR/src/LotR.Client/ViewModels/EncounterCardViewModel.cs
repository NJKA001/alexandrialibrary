using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

using LotR.Cards.Encounter;

namespace LotR.Client.ViewModels
{
    public class EncounterCardViewModel
        : ViewModelBase
    {
        public EncounterCardViewModel(Dispatcher dispatcher, IEncounterCard card)
            : base(dispatcher)
        {
            if (card == null)
                throw new ArgumentNullException("card");

            this.card = card;
            this.cardFrontImageSource = string.Format("pack://application:,,,/Images/{0}_{1}.jpg", card.CardSet, card.CardNumber);
        }

        private readonly IEncounterCard card;
        private readonly string cardFrontImageSource;
        private const string cardBackImageSource = "pack://application:,,,/Images/encounter_back.png";
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
