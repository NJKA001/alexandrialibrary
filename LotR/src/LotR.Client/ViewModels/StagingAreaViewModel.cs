using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

using LotR.States;

namespace LotR.Client.ViewModels
{
    public class StagingAreaViewModel
        : ViewModelBase
    {
        public StagingAreaViewModel(Dispatcher dispatcher, IGame game)
            : base(dispatcher)
        {
            if (game == null)
                throw new ArgumentNullException("game");

            this.game = game;

            if (game.StagingArea == null)
                throw new InvalidOperationException("staging area is undefined");

            foreach (var cardInPlay in game.StagingArea.CardsInStagingArea)
            {
                AddEncounterInPlay(cardInPlay);
            }

            this.game.StagingArea.PropertyChanged += (sender, args) => StagingAreaPropertyChanged(sender, args);
            this.game.StagingArea.RegisterCardAddedToStagingAreaCallback(x => AddEncounterInPlay(x));
            this.game.StagingArea.RegisterCardRemovedFromStagingAreaCallback(x => RemoveEncounterInPlay(x));
        }

        private readonly IGame game;
        private readonly ObservableCollection<EncounterCardInPlayViewModel> cardsInPlay = new ObservableCollection<EncounterCardInPlayViewModel>();
        private EncounterCardInPlayViewModel revealedEncounterCard;

        private void StagingAreaPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "RevealedEncounterCard":
                    RefreshRevealedEncounterCard();
                    break;
                default:
                    break;
            }
        }

        private void RefreshRevealedEncounterCard()
        {
            if (game.StagingArea.RevealedEncounterCard == null)
            {
                RevealedEncounterCard = null;
                return;
            }

            RevealedEncounterCard = new EncounterCardInPlayViewModel(dispatcher, game.StagingArea.RevealedEncounterCard);
        }

        private void AddEncounterInPlay(IEncounterInPlay encounterInPlay)
        {
            if (cardsInPlay.Any(x => x.CardId == encounterInPlay.Card.Id))
                return;

            Dispatch(() => cardsInPlay.Add(new EncounterCardInPlayViewModel(dispatcher, encounterInPlay)));
        }

        private void RemoveEncounterInPlay(IEncounterInPlay encounterInPlay)
        {
            var viewModel = cardsInPlay.Where(x => x.CardId == encounterInPlay.Card.Id).FirstOrDefault();
            if (viewModel == null)
                return;

            Dispatch(() => cardsInPlay.Remove(viewModel));
        }

        public IEnumerable<EncounterCardInPlayViewModel> CardsInPlay
        {
            get { return cardsInPlay; }
        }

        public EncounterCardInPlayViewModel RevealedEncounterCard
        {
            get { return revealedEncounterCard; }
            private set
            {
                revealedEncounterCard = value;
                OnPropertyChanged("RevealedEncounterCard");
                OnPropertyChanged("RevealedEncounterCardVisibility");
            }
        }

        public Visibility RevealedEncounterCardVisibility
        {
            get { return revealedEncounterCard != null ? Visibility.Visible : Visibility.Collapsed; }
        }
    }
}
