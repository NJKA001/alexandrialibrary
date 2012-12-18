using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

            this.game.StagingArea.RegisterCardAddedToStagingAreaCallback(x => AddEncounterInPlay(x));
            this.game.StagingArea.RegisterCardRemovedFromStagingAreaCallback(x => RemoveEncounterInPlay(x));
        }

        private readonly IGame game;
        private readonly ObservableCollection<EncounterCardInPlayViewModel> cardsInPlay = new ObservableCollection<EncounterCardInPlayViewModel>();

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
    }
}
