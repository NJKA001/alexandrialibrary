using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Heroes;
using LotR.States;

namespace LotR.Client.ViewModels
{
    public class PlayerViewModel
        : ViewModelBase
    {
        public PlayerViewModel(Dispatcher dispatcher, IGame game, IPlayer player)
            : base(dispatcher)
        {
            if (game == null)
                throw new ArgumentNullException("game");
            if (player == null)
                throw new ArgumentNullException("player");

            this.player = player;

            foreach (var hero in player.Deck.Heroes)
            {
                var heroInPlay = new HeroInPlay(game, hero);
                heroes.Add(new PlayerCardInPlayViewModel<IHeroCard>(dispatcher, heroInPlay));
            }

            player.Hand.RegisterCardAddedCallback(x => CardAddedToHand(x));
            player.Hand.RegisterCardRemovedCallback(x => CardRemovedFromHand(x));
        }

        private readonly IPlayer player;
        private readonly ObservableCollection<PlayerCardInPlayViewModel<IHeroCard>> heroes = new ObservableCollection<PlayerCardInPlayViewModel<IHeroCard>>();
        private readonly ObservableCollection<PlayerCardViewModel> hand = new ObservableCollection<PlayerCardViewModel>();

        private void CardAddedToHand(IPlayerCard card)
        {
            Dispatch(() => hand.Add(new PlayerCardViewModel(dispatcher, card)));
        }

        private void CardRemovedFromHand(IPlayerCard card)
        {
            var viewModel = hand.Where(x => x.CardId == card.Id).FirstOrDefault();
            if (viewModel == null)
                return;

            Dispatch(() => hand.Remove(viewModel));
        }

        public string PlayerName
        {
            get { return player.Name; }
        }

        public byte CurrentThreat
        {
            get { return player.CurrentThreat; }
        }

        public IEnumerable<PlayerCardInPlayViewModel<IHeroCard>> Heroes
        {
            get { return heroes; }
        }

        public IEnumerable<PlayerCardViewModel> Hand
        {
            get { return hand; }
        }
    }
}
