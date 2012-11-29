using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Client.ViewModels
{
    public class StagingAreaViewModel
        : ViewModelBase
    {
        public StagingAreaViewModel(IGame game)
        {
            if (game == null)
                throw new ArgumentNullException("game");

            this.game = game;

            if (game.StagingArea != null)
            {
                foreach (var cardInPlay in game.StagingArea.CardsInStagingArea)
                {
                    AddEncounterInPlay(cardInPlay);
                }
            }
        }

        private readonly IGame game;
        private readonly ObservableCollection<EncounterCardInPlayViewModel> cardsInPlay = new ObservableCollection<EncounterCardInPlayViewModel>();

        private void AddEncounterInPlay(IEncounterInPlay encounterInPlay)
        {
            if (cardsInPlay.Any(x => x.CardId == encounterInPlay.Card.Id))
                return;

            cardsInPlay.Add(new EncounterCardInPlayViewModel(encounterInPlay));
        }

        private void RemoveEncounterInPlay(IEncounterInPlay encounterInPlay)
        {
            var viewModel = cardsInPlay.Where(x => x.CardId == encounterInPlay.Card.Id).FirstOrDefault();
            if (viewModel == null)
                return;

            cardsInPlay.Remove(viewModel);
        }

        public IEnumerable<EncounterCardInPlayViewModel> CardsInPlay
        {
            get { return cardsInPlay; }
        }
    }
}
